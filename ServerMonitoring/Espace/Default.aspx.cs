using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.IO.Compression;
using System.IO;

using ServerMonitoring_fw;
using ServerMonitoring_fw.BIZ;
using iZyTools.Convertion;

public partial class Espace_Default : BasePage
{
	//protected PerformanceCounter cpuCounter;
	//protected PerformanceCounter ramCounterAvailable;


	protected void Page_Load(object sender, EventArgs e)
	{
		Utilisateur myUser = GetUtilisateur();
		if (myUser != null)
		{
			if (!IsPostBack)
			{
				Initialization();
			}
			PerfInit();
		}
		else
			Response.Redirect("~/Default.aspx");
	}

	#region Init
	public void Initialization()
	{
		List<Serveur> listeServeur = ServeurManager.FindAll();
		if (listeServeur.Count > 0)
		{
			lblNomServeur.Text = listeServeur[0].libelle;
			lblIPLocale.Text = listeServeur[0].ipLocale;
			lblIPPublique.Text = listeServeur[0].ipPublique;
		}

		ListeLog_Init();
		ListeBaseDonnees_Init();
		GenerateDLL();
	}

	public void ListeLog_Init()
	{
		List<Log> logs = LogManager.FindAll();

		rptLogs.DataSource = logs;
		rptLogs.DataBind();

		upLogs.Update();
	}

	public void ListeBaseDonnees_Init()
	{
		List<BaseDonnee> bdd = BaseDonneeManager.FindAll();

		rptDatabase.DataSource = bdd;
		rptDatabase.DataBind();

		upDatabase.Update();
	}

	protected void GenerateDLL()
	{
		List<Projet> projets = ProjetManager.FindAll();

		ddlEditLogProjet.DataSource = projets.OrderBy(p => p.libelle);
		ddlEditLogProjet.DataTextField = "libelle";
		ddlEditLogProjet.DataValueField = "id";
		ddlEditLogProjet.DataBind();

		ddlEditDatabaseProjet.DataSource = projets.OrderBy(p => p.libelle);
		ddlEditDatabaseProjet.DataTextField = "libelle";
		ddlEditDatabaseProjet.DataValueField = "id";
		ddlEditDatabaseProjet.DataBind();

		upLogs.Update();
	}
	#endregion

	#region Performances
	public void PerfInit()
	{
		//cpuCounter = new PerformanceCounter();

		//cpuCounter.CategoryName = "Processor";
		//cpuCounter.CounterName = "% Processor Time";
		//cpuCounter.InstanceName = "_Total";

		//ramCounterAvailable = new PerformanceCounter("Memory", "Available MBytes");
	}

	protected void timerPerf_Tick(object sender, EventArgs e)
	{
		//cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

		//ramCounterAvailable = new PerformanceCounter("Memory", "Available MBytes");


		//string firstValue = cpuCounter.NextValue() + "%";
		//Thread.Sleep(200);
		//lblCPUValeur.Text = cpuCounter.NextValue() + "%";

		//lblRAMDispoValeur.Text = ramCounterAvailable.NextValue() + "MB";
		//upPerformances.Update();


	}
	#endregion Performances

	#region LOGS
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
		ddlEditLogProjet.SelectedIndex = -1;

		upLogs.Update();
	}

	protected void btnEditLogSave_Click(object sender, EventArgs e)
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
		myLog.idProjet = iZyInt.ConvertStringToInt(ddlEditLogProjet.SelectedValue);

		if (isModif) LogManager.Update(myLog);
		else LogManager.Insert(myLog);

		ListeLog_Init();

		btnAddLog.Enabled = true;
		pnlEditLog.Visible = false;
		tbEditLogChemin.Text = string.Empty;
		ddlEditLogProjet.SelectedIndex = -1;
		// Sauvegarde
		upLogs.Update();
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
				ddlEditLogProjet.SelectedValue = myLog.idProjet.ToString();

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

	protected void rptLogs_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			// ~/Espace/ViewFile.aspx
			HyperLink btnSeeFile = (HyperLink)e.Item.FindControl("btnSeeFile");
			Log myLog = (Log)e.Item.DataItem;
			if (btnSeeFile != null && myLog != null)
			{
				btnSeeFile.NavigateUrl = "~/Espace/ViewFile.aspx" + MySession.GenerateGetParams("idLog=" + myLog.id + "&menuDisabled=1");
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
		ddlEditDatabaseProjet.SelectedIndex = -1;
		tbEditDatabaseHost.Text = string.Empty;
		tbEditDatabaseName.Text = string.Empty;
		tbEditDatabaseUser.Text = string.Empty;
		tbEditDatabasePassword.Text = string.Empty;
		tbEditDatabaseChemin.Text = string.Empty;
	}

	protected void btnEditDatabaseSave_Click(object sender, EventArgs e)
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
		myDatabase.idProjet = iZyInt.ConvertStringToInt(ddlEditDatabaseProjet.SelectedValue);

		if (isModif) BaseDonneeManager.Update(myDatabase);
		else BaseDonneeManager.Insert(myDatabase);

		ListeBaseDonnees_Init();

		ResetDatabaseForm();

		btnAddDatabase.Enabled = true;
		pnlEditDatabase.Visible = false;

		// Sauvegarde
		upDatabase.Update();
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
				ddlEditDatabaseProjet.SelectedValue = myDatabase.idProjet.ToString();

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