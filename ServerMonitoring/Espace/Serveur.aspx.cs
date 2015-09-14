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
using System.Security;

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
			InitStockage();
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
			tbCheminServerInfosExe.Text = myServeur.cheminInfosMonitoring;
		}
	}
	protected void btnEditServeur_Click(object sender, EventArgs e)
	{
		Serveur myServeur = GetServeur();
		if (myServeur != null)
		{
			myServeur.libelle = tbNomServeur.Text;
			myServeur.ipLocale = tbIPLocale.Text;
			myServeur.ipPublique = tbIPPublique.Text;
			myServeur.cheminInfosMonitoring = tbCheminServerInfosExe.Text;
			ServeurManager.Update(myServeur);
			lblMessageSauvegarde.Text = "Sauvegarde effectuée";
			upInfos.Update();
		}

	}
	#endregion

	#region CPU
	protected string GetLastCPUValue()
	{
		string val = "0";
		try
		{
			Serveur myServeur = GetServeur();
			if (myServeur != null)
			{
				LogMonitoring.Add("-- Début GetLastCPUValue --");
				string execPath = myServeur.cheminInfosMonitoring;
				string resPath = Path.Combine(Path.GetDirectoryName(execPath), "cpu.txt");

				ProcessStartInfo psi = new ProcessStartInfo();
				psi.FileName = execPath;
				psi.Arguments = "cpu";
				psi.CreateNoWindow = true;
				psi.WindowStyle = ProcessWindowStyle.Hidden;
				psi.UserName = "QVYLMZ8L03CTZE";
				psi.Password = ConvertToSecureString("LZ0i0GODt9cuoJ");
				psi.UseShellExecute = false;

				LogMonitoring.Add("Lancement ProcessStartInfo");
				Process proc = Process.Start(psi);
				proc.WaitForExit();
				LogMonitoring.Add("Fin ProcessStartInfo");

				val = File.ReadAllText(resPath);
				val = val.Replace(",", ".");
				LogMonitoring.Add("CPU : " + val);
				LogMonitoring.Add("-- Fin GetLastCPUValue --");
			}
		}
		catch (Exception e)
		{
			LogMonitoring.Add("ERROR : GetLastCPUValue : " + e.Message);
			LogMonitoring.Add(e.StackTrace);
		}
		return val;
	}

	protected void InitCPU()
	{
		string lastCPUStr = GetLastCPUValue();
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
			Serveur myServeur = GetServeur();
			if (myServeur != null)
			{
				LogMonitoring.Add("-- Début GetLastRAMValue --");
				string execPath = myServeur.cheminInfosMonitoring;
				string resPath = Path.Combine(Path.GetDirectoryName(execPath), "ram.txt");
				LogMonitoring.Add("Préparation ProcessStartInfo");
				ProcessStartInfo psi = new ProcessStartInfo();
				psi.FileName = execPath;
				psi.Arguments = "ram";
				psi.CreateNoWindow = true;
				psi.WindowStyle = ProcessWindowStyle.Hidden;
				psi.UserName = "QVYLMZ8L03CTZE";
				psi.Password = ConvertToSecureString("LZ0i0GODt9cuoJ");
				psi.UseShellExecute = false;

				LogMonitoring.Add("Lancement ProcessStartInfo");
				Process proc = Process.Start(psi);
				proc.WaitForExit();
				
				LogMonitoring.Add("Fin ProcessStartInfo");

				val = File.ReadAllText(resPath);
				LogMonitoring.Add("RAM : " + val);
				LogMonitoring.Add("-- Fin GetLastRAMValue --");
			}
		}
		catch (Exception e)
		{
			LogMonitoring.Add("ERROR : GetLastRAMValue : " + e.Message);
			LogMonitoring.Add(e.StackTrace);
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
		pnlRAMCircle.CssClass = "c100 green p" + pourcentageRound + " big";
		lblRAMValue.Text = pourcentageRound + "%";
	}
	#endregion RAM

	#region ESPACE DISQUE
	class Disque
	{
		public string Lettre;
		public long TotalSize;
		public long TotalFree;

		public bool IsOk;

		public long Pourcentage
		{
			get
			{
				long pct = TotalFree * 100 / TotalSize;
				return pct;
			}
		}

		public Disque(string ligne)
		{
			try
			{
				string[] ligneTab = ligne.Split(' ');
				if (ligneTab.Length == 3)
				{
					Lettre = ligneTab[0];
					TotalSize = Convert.ToInt64(ligneTab[1]);
					TotalFree = Convert.ToInt64(ligneTab[2]);
					IsOk = true;
				}
				else
					IsOk = false;
			}
			catch (Exception ex)
			{
				IsOk = false;
			}
		}

		public string GetHTMLCode()
		{
			string code = string.Empty;
			if (IsOk)
			{
				code += "<div class='c100 orange p" + Pourcentage + "'>";
				code += "<span>" + Lettre + " " + Pourcentage + "%</span>";
				code += "<div class='slice'>";
				code += "<div class='bar'></div>";
				code += "<div class='fill'></div>";
				code += "</div>";
				code += "</div>";
			}
			return code;
		}

	}

	protected List<string> GetLastEspaceDisque()
	{
		List<string> val = new List<string>();
		try
		{
			Serveur myServeur = GetServeur();
			if (myServeur != null)
			{
				LogMonitoring.Add("-- Début GetLastEspaceDisque --");
				string execPath = myServeur.cheminInfosMonitoring;

				string resPath = Path.Combine(Path.GetDirectoryName(execPath), "disk.txt");

				ProcessStartInfo psi = new ProcessStartInfo();
				psi.FileName = execPath;
				psi.Arguments = "disk";
				psi.CreateNoWindow = true;
				psi.WindowStyle = ProcessWindowStyle.Hidden;
				psi.UserName = "QVYLMZ8L03CTZE";
				psi.Password = ConvertToSecureString("LZ0i0GODt9cuoJ");
				psi.UseShellExecute = false;

				LogMonitoring.Add("Lancement ProcessStartInfo");
				Process proc = Process.Start(psi);
				proc.WaitForExit();
				LogMonitoring.Add("Fin ProcessStartInfo");

				val = File.ReadAllLines(resPath).ToList();
				LogMonitoring.Add("DISK : " + val);
				LogMonitoring.Add("-- Fin GetLastEspaceDisque --");
			}
		}
		catch (Exception e)
		{
			LogMonitoring.Add("ERROR : GetLastEspaceDisque : " + e.Message);
			LogMonitoring.Add(e.StackTrace);
		}
		return val;
	}

	protected void InitStockage()
	{
		List<string> currentDiskSpaces = GetLastEspaceDisque();
		litStockages.Text = string.Empty;
		foreach (string ligne in currentDiskSpaces)
		{
			Disque myDisk = new Disque(ligne);
			if (myDisk.IsOk)
			{
				litStockages.Text += myDisk.GetHTMLCode();
			}
		}
	}

	#endregion

	protected void timerMonitoring_Tick(object sender, EventArgs e)
	{
		//FillRAMValues();
		//InitChartRAM();

		//FillCPUValues();
		//InitChartCPU();
		LogMonitoring.Add("Tick");
		InitCPU();
		InitRAM();
		InitStockage();

		upMonitoring.Update();
	}

	private SecureString ConvertToSecureString(string password)
	{
		if (password == null)
			throw new ArgumentNullException("password");

		var securePassword = new SecureString();
		foreach (char c in password)
			securePassword.AppendChar(c);
		securePassword.MakeReadOnly();
		return securePassword;
	}
}