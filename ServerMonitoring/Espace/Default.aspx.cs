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
				Initialization(myUser);
			}
			PerfInit();
		}
		else
			Response.Redirect("~/Default.aspx");
	}

	#region Init
	public void Initialization(Utilisateur myUser)
	{
		if (myUser != null)
		{
			List<Serveur> listeServeur = ServeurManager.FindAll();
			if (listeServeur.Count > 0)
			{
				lblNomServeur.Text = listeServeur[0].libelle;
				lblIPLocale.Text = listeServeur[0].ipLocale;
				lblIPPublique.Text = listeServeur[0].ipPublique;
			}

			ListeLog_Init(myUser);
			ListeBaseDonnees_Init(myUser);
		}
	}

	public void ListeLog_Init(Utilisateur myUser)
	{
		if (myUser != null)
		{
			List<Log> logs = myUser.myLogFavoris;

			rptLogs.DataSource = logs;
			rptLogs.DataBind();

			upLogs.Update();
		}
	}

	public void ListeBaseDonnees_Init(Utilisateur myUser)
	{
		if (myUser != null)
		{
			List<BaseDonnee> bdd = myUser.myBaseDonneeFavoris;

			rptDatabase.DataSource = bdd;
			rptDatabase.DataBind();

			upDatabase.Update();
		}
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