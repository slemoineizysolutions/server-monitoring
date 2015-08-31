using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServerMonitoring_fw;
using ServerMonitoring_fw.BIZ;
using iZyTools.Convertion;
using System.Diagnostics;
using System.IO;

public partial class Espace_Serveur : BasePage
{
	protected List<string> CPUValues
	{
		get
		{
			return (List<string>)MySession.GetSession("Serveur-CPU-Values");
		}
		set
		{
			MySession.SetSession("Serveur-CPU-Values", value);
		}
	}
	protected List<string> RAMValues
	{
		get
		{
			return (List<string>)MySession.GetSession("Serveur-RAM-Values");
		}
		set
		{
			MySession.SetSession("Serveur-RAM-Values", value);
		}
	}

	protected int TotalRAM
	{
		get
		{
			return (int)MySession.GetSession("Serveur-RAM-Total");
		}
		set
		{
			MySession.SetSession("Serveur-RAM-Total", value);
		}
	}

	protected Serveur GetServeur()
	{
		return ServeurManager.Load(iZyInt.ConvertStringToInt(MySession.GetParam("id")));
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
		hlBackProjet.NavigateUrl = "~/Espace/Serveurs.aspx" + MySession.GenerateGetParams();

		Serveur myServeur = GetServeur();
		if (myServeur != null)
		{
			projetName.Text = myServeur.libelle;

			InfosGenerales_Init(myServeur);

			Microsoft.VisualBasic.Devices.ComputerInfo cptInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
			ulong totalRam = cptInfo.TotalPhysicalMemory;
			TotalRAM = Convert.ToInt32((totalRam / 1024) / 1024);
			//TotalRAM = Convert.ToInt32(cptInfo.TotalPhysicalMemory);

			InitCPU();
			InitRAM();
		}
	}

	#region Infos Générales
	protected void InfosGenerales_Init(Serveur myServeur = null)
	{
		if (myServeur == null) myServeur = GetServeur();
		if (myServeur != null)
		{
			tbNomServeur.Text = myServeur.libelle;
			tbIPLocale.Text = myServeur.ipLocale;
			tbIPPublique.Text = myServeur.ipPublique;
		}
	}
	protected void btnEditServeur_Click(object sender, EventArgs e)
	{

	}
	#endregion

	#region CPU
	protected string GetLastCPUValue()
	{
		string val = "0";
		try
		{
			string execPath = @"D:\Github\server-monitoring\ServerInfosMonitoring\bin\Debug\ServerInfosMonitoring.exe";

			string resPath = Path.Combine(Path.GetDirectoryName(execPath), "cpu.txt");

			ProcessStartInfo psi = new ProcessStartInfo();
			psi.FileName = execPath;
			psi.Arguments = "cpu";
			psi.CreateNoWindow = true;
			psi.WindowStyle = ProcessWindowStyle.Hidden;

			Process proc = Process.Start(psi);
			proc.WaitForExit();

			val = File.ReadAllText(resPath);
			val = val.Replace(",", ".");
		}
		catch (Exception e)
		{

		}
		return val;
	}

	protected void InitCPU()
	{
		string lastCPUStr = GetLastCPUValue().Replace(".", ",");
		decimal lastCPU = Decimal.Parse(lastCPUStr);
		decimal lastCPURound = Math.Round(lastCPU);
		pnlCPuCircle.CssClass = "c100 p" + lastCPURound + " big";
		lblCPUValue.Text = lastCPURound + "%";
	}
	#endregion CPU

	#region RAM

	protected string GetLastRAMValue()
	{
		string val = "0";
		try
		{
			string execPath = @"D:\Github\server-monitoring\ServerInfosMonitoring\bin\Debug\ServerInfosMonitoring.exe";

			string resPath = Path.Combine(Path.GetDirectoryName(execPath), "ram.txt");

			ProcessStartInfo psi = new ProcessStartInfo();
			psi.FileName = execPath;
			psi.Arguments = "ram";
			psi.CreateNoWindow = true;
			psi.WindowStyle = ProcessWindowStyle.Hidden;

			Process proc = Process.Start(psi);
			proc.WaitForExit();

			val = File.ReadAllText(resPath);
		}
		catch (Exception e)
		{

		}
		return val;
	}

	protected void InitChartRAM()
	{
		List<string> ramValues = RAMValues;
		string script = string.Empty;

		if (ramValues != null && ramValues.Count > 0)
		{
			script += "var lineChartRAM = {";
			script += "labels : ['0 sec','";
			for (int i = 0; i < ramValues.Count - 1; i++)
			{
				if (i == ramValues.Count - 2)
					script += "60 sec'],";
				else
					script += "','";
			}

			script += "datasets : [";
			script += "{";
			script += "label: 'RAM',";
			//script += "title: 'CPU',";
			script += "fillColor : 'rgba(169,68,24,0.2)',";
			script += "strokeColor : 'rgba(169,68,24,1)',";
			script += "pointColor : 'rgba(169,68,24,1)',";
			script += "pointStrokeColor : '#fff',";
			script += "pointHighlightFill : '#fff',";
			script += "pointHighlightStroke : 'rgba(169,68,24,1)',";
			script += "data : [";
			for (int i = 0; i < ramValues.Count; i++)
			{
				if (i == ramValues.Count - 1)
					script += ramValues[i] + "]";
				else
					script += ramValues[i] + ",";
			}
			script += "}";
			script += "]";
			script += "};";

			script += "function chartRAM(){";
			script += "var ctx = document.getElementById('canvasRAM').getContext('2d');";
			script += "window.myLine = new Chart(ctx).Line(lineChartRAM, {";
			script += "responsive: true, bezierCurve : false, animation: false";
			script += "});";
			script += "}";

			script += "$(function(){";
			script += "chartRAM();";
			script += "});";

			script += "Sys.WebForms"; /** add_endRequest pour relancer la fonction lors d'updates partielles, on fait un remove_endRequest avant au cas où (peut-être sans intéret ...) **/
			script += ".PageRequestManager";
			script += ".getInstance()";
			script += ".remove_endRequest(chartRAM);";
			script += "Sys.WebForms";
			script += ".PageRequestManager";
			script += ".getInstance()";
			script += ".add_endRequest(chartRAM);";

			ScriptManager.RegisterStartupScript(this, this.GetType(), "chartRAM", script, true);
		}
	}

	protected void InitRAM()
	{
		string lastRAMStr = GetLastRAMValue();
		int lastRAM = Convert.ToInt32(lastRAMStr);

		int pourcentage = lastRAM * 100 / TotalRAM;
		int pourcentageRound = pourcentage;
		pnlRAMCircle.CssClass = "c100 p" + pourcentageRound + " big";
		lblRAMValue.Text = pourcentageRound + "%";
	}
	#endregion RAM

	protected void timerMonitoring_Tick(object sender, EventArgs e)
	{
		//FillRAMValues();
		//InitChartRAM();

		//FillCPUValues();
		//InitChartCPU();

		InitCPU();
		InitRAM();

		upMonitoring.Update();
	}
}