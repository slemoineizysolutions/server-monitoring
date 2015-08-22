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
            if (args.Length >= 1)
            {

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

        public static void PublishResultats(string res, string nameResults)
        {
            if (!string.IsNullOrEmpty(res) && !string.IsNullOrEmpty(nameResults))
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nameResults + ".txt"), res);
            }
        }

        public static string GetMemoryAvailable()
        {
            ramCounterAvailable = new PerformanceCounter("Memory", "Available MBytes");
            string firstValue = ramCounterAvailable.NextValue() + "%";
            Thread.Sleep(200);
            return ramCounterAvailable.NextValue().ToString();
        }

        public static string GetCPU()
        {
            cpuCounter = new PerformanceCounter();

            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            return cpuCounter.NextValue().ToString();
        }

        public static string GetDiskSpace()
        {
            string res = string.Empty;
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady) res += drive.Name + " " + drive.TotalSize + " " + drive.TotalFreeSpace + Environment.NewLine;
                //drive.TotalFreeSpace
            }
            return res;
        }
    }
}
