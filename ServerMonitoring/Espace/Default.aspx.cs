using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

using ServerMonitoring_fw;
using iZyTools.Convertion;

public partial class Espace_Default : BasePage
{
	protected PerformanceCounter cpuCounter;
	protected PerformanceCounter ramCounterAvailable;


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
		GenerateDLL();
	}

	public void ListeLog_Init()
	{
		List<Log> logs = LogManager.FindAll();

		rptLogs.DataSource = logs;
		rptLogs.DataBind();

		upLogs.Update();
	}

	protected void GenerateDLL()
	{
		List<Projet> projets = ProjetManager.FindAll();

		ddlEditLogProjet.DataSource = projets.OrderBy(p => p.libelle);
		ddlEditLogProjet.DataTextField = "libelle";
		ddlEditLogProjet.DataValueField = "id";
		ddlEditLogProjet.DataBind();

		upLogs.Update();
	}
	#endregion

	#region Performances
	public void PerfInit()
	{
		cpuCounter = new PerformanceCounter();

		cpuCounter.CategoryName = "Processor";
		cpuCounter.CounterName = "% Processor Time";
		cpuCounter.InstanceName = "_Total";

		ramCounterAvailable = new PerformanceCounter("Memory", "Available MBytes");
	}

	protected void timerPerf_Tick(object sender, EventArgs e)
	{
		cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

		ramCounterAvailable = new PerformanceCounter("Memory", "Available MBytes");


		string firstValue = cpuCounter.NextValue() + "%";
		Thread.Sleep(200);
		lblCPUValeur.Text = cpuCounter.NextValue() + "%";

		lblRAMDispoValeur.Text = ramCounterAvailable.NextValue() + "MB";
		upPerformances.Update();


	}
	#endregion Performances

	#region LOGS
	protected void btnAddLog_Click(object sender, EventArgs e)
	{
		pnlEditLog.Visible = true;
		btnAddLog.Enabled = false;

		upLogs.Update();
	}

	protected void btnEditLogAnnuler_Click(object sender, EventArgs e)
	{
		btnAddLog.Enabled = true;
		pnlEditLog.Visible = false;
		tbEditLogChemin.Text = string.Empty;
		ddlEditLogProjet.SelectedIndex = -1;

		upLogs.Update();
	}

	protected void btnEditLogSave_Click(object sender, EventArgs e)
	{
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

	}
	#endregion
	protected void rptLogs_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item)
		{
			// ~/Espace/ViewFile.aspx
			HyperLink btnSeeFile = (HyperLink)e.Item.FindControl("btnSeeFile");
			Log myLog = (Log)e.Item.DataItem;
			if (btnSeeFile != null && myLog != null)
			{
				btnSeeFile.NavigateUrl = "~/Espace/ViewFile.aspx" + MySession.GenerateGetParams("idLog=" + myLog.id);
			}
		}

	}
}