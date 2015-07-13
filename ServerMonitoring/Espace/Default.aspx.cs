﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

using ServerMonitoring_fw;

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
				Init();
			}
			PerfInit();
		}
		else
			Response.Redirect("~/Default.aspx");
	}

	public void Init()
	{
		List<Serveur> listeServeur = ServeurManager.FindAll();
		if (listeServeur.Count > 0)
		{
			lblNomServeur.Text = listeServeur[0].libelle;
			lblIPLocale.Text = listeServeur[0].ipLocale;
			lblIPPublique.Text = listeServeur[0].ipPublique;
		}
	}

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
		pnlEditLog.Visible = true;
		btnAddLog.Enabled = false;
		upLogs.Update();
	}

	#endregion
}