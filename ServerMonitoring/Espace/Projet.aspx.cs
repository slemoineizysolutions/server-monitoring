using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.IO.Compression;
using System.Web.UI.WebControls;

using ServerMonitoring_fw;
using ServerMonitoring_fw.BIZ;
using iZyTools.Convertion;

public partial class Espace_Projet : BasePage
{
	protected List<Log> ListeLogFavoris { get; set; }
	protected List<BaseDonnee> ListeBaseDonneeFavoris { get; set; }

	protected Projet GetProjet()
	{
		return ProjetManager.Load(iZyInt.ConvertStringToInt(MySession.GetParam("id")));
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		Utilisateur myUser = GetUtilisateur();
		if (myUser != null)
		{
			if (!IsPostBack)
			{
				Initialization();
			}
		}
		else
			Response.Redirect("~/Default.aspx");
	}

	public void Initialization()
	{
		hlBackProjet.NavigateUrl = "~/Espace/Projets.aspx" + MySession.GenerateGetParams();

		Projet myProjet = GetProjet();
		if (myProjet != null)
		{
			projetName.Text = myProjet.libelle;
			pnlPageTitle.CssClass = "page-title " + myProjet.myTheme.cssClass;

			InfosGenerales_Init(myProjet);
			ListeLog_Init(myProjet);
			ListeBaseDonnees_Init(myProjet);
		}
	}

	protected void InfosGenerales_Init(Projet myProjet = null)
	{
		if (myProjet == null) myProjet = GetProjet();
		if (myProjet != null)
		{
			tbUrlProd.Text = myProjet.urlProd;
			tbUrlTest.Text = myProjet.urlTest;

			List<EnumTheme> listeTheme = EnumThemeManager.FindAll();
			switchTheme.ClearOptionItems();
			foreach (EnumTheme myTheme in listeTheme)
			{
				switchTheme.AddOption(new iZyWebServerControl.OptionItem(myTheme.id.ToString(), myTheme.libelle, false, myTheme.cssClass));
			}
			switchTheme.SelectedValue = myProjet.idTheme.ToString();
		}
	}

	public void ListeLog_Init(Projet myProjet = null)
	{
		Utilisateur myUser = GetUtilisateur();
		if (myProjet == null) myProjet = GetProjet();
		if (myProjet != null)
		{
			ListeLogFavoris = LogManager.FindFavoris(myUser.id);
			List<Log> logs = LogManager.FindAll(myProjet.id);

			rptLogs.DataSource = logs;
			rptLogs.DataBind();

			
		}
		upLogs.Update();
	}

	public void ListeBaseDonnees_Init(Projet myProjet = null)
	{
		if (myProjet == null) myProjet = GetProjet();
		if (myProjet != null)
		{
			List<BaseDonnee> bdd = BaseDonneeManager.FindAll(myProjet.id);

			rptDatabase.DataSource = bdd;
			rptDatabase.DataBind();

			upDatabase.Update();
		}
	}

	#region Infos générales
	protected void btnEditProjet_Click(object sender, EventArgs e)
	{
		Projet myProjet = GetProjet();
		if (myProjet != null)
		{
			bool allReload = false;
			try
			{
				myProjet.urlProd = tbUrlProd.Text;
				myProjet.urlTest = tbUrlTest.Text;
				int newTheme = iZyInt.ConvertStringToInt(switchTheme.SelectedValue);
				if (newTheme != myProjet.idTheme) allReload = true;
				myProjet.idTheme = newTheme;

				ProjetManager.Update(myProjet);
				lblMessageSauvegarde.Text = "Sauvegarde réussie";
				lblMessageSauvegarde.CssClass = "projet-save-message success";
			}
			catch (Exception exc)
			{
				// log
				lblMessageSauvegarde.Text = "Une erreur s'est produite durant la sauvegarde";
				lblMessageSauvegarde.CssClass = "projet-save-message error";
			}
			if (allReload)
			{
				Initialization();
				upGeneral.Update();
			}
			else upInfos.Update();
		}
	}

	#endregion

	#region Logs
	protected void btnAddLog_Click(object sender, EventArgs e)
	{
		hfLogId.Value = string.Empty;
		pnlEditLog.Visible = true;
		btnAddLog.Enabled = false;

		upLogs.Update();
	}

	protected void btnEditLogAnnuler_Click(object sender, EventArgs e)
	{
		btnAddLog.Enabled = true;
		pnlEditLog.Visible = false;
		tbEditLogChemin.Text = string.Empty;
		tbEditLogLibelle.Text = string.Empty;

		upLogs.Update();
	}

	protected void btnEditLogSave_Click(object sender, EventArgs e)
	{
		Projet myProjet = GetProjet();
		if (myProjet != null)
		{
			bool isModif = false;
			Log myLog = new Log();
			if (!string.IsNullOrEmpty(hfLogId.Value))
			{
				myLog = LogManager.Load(iZyInt.ConvertStringToInt(hfLogId.Value));
				if (myLog != null) isModif = true;
				else myLog = new Log();
			}

			myLog.libelle = tbEditLogLibelle.Text;
			myLog.cheminFichier = tbEditLogChemin.Text;
			myLog.idProjet = myProjet.id;

			if (isModif) LogManager.Update(myLog);
			else LogManager.Insert(myLog);

			ListeLog_Init();

			btnAddLog.Enabled = true;
			pnlEditLog.Visible = false;
			tbEditLogChemin.Text = string.Empty;
			// Sauvegarde
			upLogs.Update();
		}
	}

	protected void btnConfigLog_Click(object sender, EventArgs e)
	{
		LinkButton btn = (LinkButton)sender;
		if (btn != null)
		{
			Log myLog = LogManager.Load(iZyInt.ConvertStringToInt(btn.CommandArgument));

			if (myLog != null)
			{
				hfLogId.Value = btn.CommandArgument;
				tbEditLogLibelle.Text = myLog.libelle;
				tbEditLogChemin.Text = myLog.cheminFichier;

				pnlEditLog.Visible = true;
				btnAddLog.Enabled = false;

			}
		}
		upLogs.Update();
	}

	protected void btnDownloadFichier_Click(object sender, EventArgs e)
	{
		LinkButton btnDownloadFichier = (LinkButton)sender;
		if (btnDownloadFichier != null)
		{
			int idLog = iZyInt.ConvertStringToInt(btnDownloadFichier.CommandArgument);
			Log myLog = LogManager.Load(idLog);
			if (myLog != null)
			{
				if (File.Exists(myLog.cheminFichier))
				{
					string fileName = "log-" + myLog.myProjet.libelle + "-" + myLog.libelle + "-" + DateTime.Now.ToString("yyyyMMddhhmmss");
					string zipfile = Path.Combine(Param.TMP, fileName + ".zip");
					if (File.Exists(zipfile))
					{
						File.Delete(zipfile);
					}

					using (ZipArchive zip = ZipFile.Open(zipfile, ZipArchiveMode.Create))
					{
						zip.CreateEntryFromFile(myLog.cheminFichier, fileName + ".log");
					}

					if (File.Exists(zipfile))
					{
						Response.AppendHeader("content-disposition", "attachment; filename=" + fileName + "_pdf.zip");
						Response.ContentType = "application/zip";
						Response.WriteFile(zipfile);
					}
				}
			}
		}
	}

	protected void btnFavorisLog_Click(object sender, EventArgs e)
	{
		LinkButton btn = (LinkButton)sender;
		Utilisateur myUser = GetUtilisateur();
		if (btn != null && myUser != null)
		{
			int idLog = iZyInt.ConvertStringToInt(btn.CommandArgument);
			if (btn.CommandName == "1") // on retire le favoris
			{
				UtilisateurFavorisManager.Delete(myUser.id, idLog, EnumTypeFavoris.LOG);
			}
			else // on rajoute un favoris
			{
				UtilisateurFavoris myFav = new UtilisateurFavoris() { idUtilisateur = myUser.id, idEntite = idLog, idType = EnumTypeFavoris.LOG };
				UtilisateurFavorisManager.Insert(myFav);
			}
		}
		ListeLog_Init();
	}

	protected void rptLogs_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			Log myLog = (Log)e.Item.DataItem;
			if (myLog != null)
			{
				// ~/Espace/ViewFile.aspx
				HyperLink btnSeeFile = (HyperLink)e.Item.FindControl("btnSeeFile");

				if (btnSeeFile != null)
				{
					btnSeeFile.NavigateUrl = "~/Espace/ViewFile.aspx" + MySession.GenerateGetParams("idLog=" + myLog.id + "&menuDisabled=1");
				}

				LinkButton btnFavorisLog = (LinkButton)e.Item.FindControl("btnFavorisLog");
				if (ListeLogFavoris != null && btnFavorisLog != null)
				{
					if (ListeLogFavoris.Find(l => l.id == myLog.id) != null)
					{
						Control favOn = e.Item.FindControl("logFavorisOn");
						if (favOn != null) favOn.Visible = true;
						btnFavorisLog.CommandName = "1";
					}
					else
					{
						Control favOff = e.Item.FindControl("logFavorisOff");
						if (favOff != null) favOff.Visible = true;
						btnFavorisLog.CommandName = "0";
					}
				}
			}
		}

	}


	#endregion

	#region Base de données
	protected void btnAddDatabase_Click(object sender, EventArgs e)
	{
		hfDatabseId.Value = string.Empty;
		pnlEditDatabase.Visible = true;
		btnAddDatabase.Enabled = false;

		upDatabase.Update();
	}

	protected void btnEditDatabaseAnnuler_Click(object sender, EventArgs e)
	{
		ResetDatabaseForm();

		btnAddDatabase.Enabled = true;
		pnlEditDatabase.Visible = false;

		upDatabase.Update();
	}

	private void ResetDatabaseForm()
	{
		tbEditDatabaseLibelle.Text = string.Empty;
		tbEditDatabaseHost.Text = string.Empty;
		tbEditDatabaseName.Text = string.Empty;
		tbEditDatabaseUser.Text = string.Empty;
		tbEditDatabasePassword.Text = string.Empty;
		tbEditDatabaseChemin.Text = string.Empty;
	}

	protected void btnEditDatabaseSave_Click(object sender, EventArgs e)
	{
		Projet myProjet = GetProjet();
		if (myProjet != null)
		{
			bool isModif = false;
			BaseDonnee myDatabase = new BaseDonnee();
			if (!string.IsNullOrEmpty(hfDatabseId.Value))
			{
				myDatabase = BaseDonneeManager.Load(iZyInt.ConvertStringToInt(hfDatabseId.Value));
				if (myDatabase != null) isModif = true;
				else myDatabase = new BaseDonnee();
			}

			myDatabase.host = tbEditDatabaseHost.Text;
			myDatabase.databaseName = tbEditDatabaseName.Text;
			myDatabase.user = tbEditDatabaseUser.Text;
			myDatabase.password = tbEditDatabasePassword.Text;
			myDatabase.cheminSauvegarde = tbEditDatabaseChemin.Text;
			myDatabase.idProjet = myProjet.id;
			myDatabase.libelle = tbEditDatabaseLibelle.Text;

			if (isModif) BaseDonneeManager.Update(myDatabase);
			else BaseDonneeManager.Insert(myDatabase);

			ListeBaseDonnees_Init();

			ResetDatabaseForm();

			btnAddDatabase.Enabled = true;
			pnlEditDatabase.Visible = false;

			// Sauvegarde
			upDatabase.Update();
		}
	}


	protected void btnConfigDatabase_Click(object sender, EventArgs e)
	{
		LinkButton btn = (LinkButton)sender;
		if (btn != null)
		{
			BaseDonnee myDatabase = BaseDonneeManager.Load(iZyInt.ConvertStringToInt(btn.CommandArgument));

			if (myDatabase != null)
			{
				hfDatabseId.Value = btn.CommandArgument;
				tbEditDatabaseHost.Text = myDatabase.host;
				tbEditDatabaseName.Text = myDatabase.databaseName;
				tbEditDatabaseUser.Text = myDatabase.user;
				tbEditDatabasePassword.Text = myDatabase.password;
				tbEditDatabaseChemin.Text = myDatabase.cheminSauvegarde;
				tbEditDatabaseLibelle.Text = myDatabase.libelle;

				pnlEditDatabase.Visible = true;
				btnAddDatabase.Enabled = false;

			}
		}
		upDatabase.Update();
	}

	protected void btnSaveDatabase_Click(object sender, EventArgs e)
	{
		LinkButton btn = (LinkButton)sender;
		if (btn != null && !string.IsNullOrEmpty(btn.CommandArgument))
		{
			int idDatabase = iZyInt.ConvertStringToInt(btn.CommandArgument);
			BaseDonnee myDatabase = BaseDonneeManager.Load(idDatabase);
			if (myDatabase != null)
			{
				// sauvegarde simple
				if (!string.IsNullOrEmpty(BaseDonneeManager.Sauvegarde(myDatabase)))
				{
					// afficher erreur car pas de sauvegarde
				}
			}
		}
	}

	protected void btnDownload_Click(object sender, EventArgs e)
	{
		LinkButton btn = (LinkButton)sender;
		if (btn != null && !string.IsNullOrEmpty(btn.CommandArgument))
		{
			int idDatabase = iZyInt.ConvertStringToInt(btn.CommandArgument);
			BaseDonnee myDatabase = BaseDonneeManager.Load(idDatabase);
			if (myDatabase != null)
			{
				// export + récupération du chemin du fichier
				string cheminZip = BaseDonneeManager.Sauvegarde(myDatabase, true);

				if (File.Exists(cheminZip))
				{
					Response.AppendHeader("content-disposition", "attachment; filename=" + Path.GetFileName(cheminZip));
					Response.ContentType = "application/zip";
					Response.WriteFile(cheminZip);
				}
			}
		}
	}
	#endregion


}