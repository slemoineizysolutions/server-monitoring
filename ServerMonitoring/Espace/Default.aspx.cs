using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Espace_Default : BasePage
{
	protected PerformanceCounter cpuCounter;
	protected PerformanceCounter ramCounter;
	protected PerformanceCounter ramCounterAvailable;

	public string getCurrentCpuUsage()
	{
		return cpuCounter.NextValue() + "%";
	}

	public string getAvailableRAM()
	{
		return ramCounterAvailable.NextValue() + "MB";
	}

	public string getRAM()
	{
		return ramCounter.NextValue() + "MB";
	} 

    protected void Page_Load(object sender, EventArgs e)
    {
		//Utilisateur myUser = GetUtilisateur();
		//if (myUser != null)
		//{
		if (!IsPostBack)
		{
			Init();
		}
		//PerfInit();
		//}
		//else
		//	Response.Redirect("~/Default.aspx");
    }

	public void Init()
	{
		lblNomServeur.Text = "MARS";
		lblIPLocale.Text = "172.15.19.56";
		lblIPPublique.Text = "82.25.156.23";

		lblCPUValeur.Text = "5.26%";
		//lblRAMValeur.Text = "7023MB";

		
	}

	public void PerfInit()
	{
		cpuCounter = new PerformanceCounter();

		cpuCounter.CategoryName = "Processor";
		cpuCounter.CounterName = "% Processor Time";
		cpuCounter.InstanceName = "_Total";

		ramCounterAvailable = new PerformanceCounter("Memory", "Available MBytes");
		ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
	}

	protected void timerPerf_Tick(object sender, EventArgs e)
	{
		cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

		//cpuCounter.CategoryName = "Processor";
		//cpuCounter.CounterName = "% Processor Time";
		//cpuCounter.InstanceName = "_Total";

		ramCounterAvailable = new PerformanceCounter("Memory", "Available MBytes");
		//ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");

		lblCPUValeur.Text = cpuCounter.NextValue() + "%";
		//lblRAMValeur.Text = getRAM();
		lblRAMDispoValeur.Text = ramCounterAvailable.NextValue() + "MB";
		upPerformances.Update();
	}
}