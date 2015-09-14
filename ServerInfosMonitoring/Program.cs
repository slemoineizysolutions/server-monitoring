using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace ServerInfosMonitoring
{
	class Program
	{
		protected static PerformanceCounter cpuCounter;
		protected static PerformanceCounter ramCounterAvailable;

		static void Main(string[] args)
		{
			try
			{
				test();
				if (args.Length >= 1)
				{
					Log.Add("arg : "+args[0]);
					switch (args[0])
					{
						case "ram":
							PublishResultats(GetMemoryAvailable(), "ram");
							break;
						case "cpu":
							PublishResultats(GetCPU(), "cpu");
							break;
						case "disk":
							PublishResultats(GetDiskSpace(), "disk");
							break;
						default:
							break;
					}
				}
			}
			catch (Exception e)
			{
				Log.Add("ERROR : " + e.Message);
				Log.Add(e.StackTrace);
			}
		}


		public static void test()
		{
			Microsoft.VisualBasic.Devices.ComputerInfo toto = new Microsoft.VisualBasic.Devices.ComputerInfo();
			ulong totalRam = toto.TotalPhysicalMemory;
		}

		public static void PublishResultats(string res, string nameResults)
		{
			if (!string.IsNullOrEmpty(res) && !string.IsNullOrEmpty(nameResults))
			{
				File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nameResults + ".txt"), res);
			}
		}

		public static string GetMemoryAvailable()
		{
			Log.Add("-- Debut GetMemoryAvailable --");
			ramCounterAvailable = new PerformanceCounter("Memory", "Available MBytes");
			string firstValue = ramCounterAvailable.NextValue() + "%";
			Thread.Sleep(100);
			string value = ramCounterAvailable.NextValue().ToString();
			Log.Add("RAM = "+value);
			Log.Add("-- Fin GetMemoryAvailable --");
			return value;
		}

		public static string GetCPU()
		{
			Log.Add("-- Debut GetCPU --");
			cpuCounter = new PerformanceCounter();

			cpuCounter.CategoryName = "Processor";
			cpuCounter.CounterName = "% Processor Time";
			cpuCounter.InstanceName = "_Total";

			cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
			string firstValue = cpuCounter.NextValue() + "%";
			Thread.Sleep(100);
			string value = cpuCounter.NextValue().ToString();
			Log.Add("CPU = " + value);
			Log.Add("-- Fin GetCPU --");

			return value;
		}

		public static string GetDiskSpace()
		{
			Log.Add("-- Debut GetDiskSpace --");
			string res = string.Empty;
			DriveInfo[] drives = DriveInfo.GetDrives();
			foreach (DriveInfo drive in drives)
			{
				if (drive.IsReady) res += drive.Name + " " + drive.TotalSize + " " + drive.TotalFreeSpace + Environment.NewLine;
				//drive.TotalFreeSpace
			}
			Log.Add("Disk = " + res);
			Log.Add("-- Fin GetDiskSpace --");
			return res;
		}
	}

	public class Log
	{
		public static string FileName
		{
			get
			{
				return "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
			}
		}

		public static void Add(string line)
		{
			StreamWriter sr = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName), true);
			line = DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss - ") + line;
			sr.WriteLine(line);
			sr.Close();
		}
	}
}
