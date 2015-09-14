using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitoring_fw.BIZ
{
	public class LogMonitoring
	{
		public static string FileName
		{
			get
			{
				return "Monitoring-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
			}
		}

		public static void Add(string line)
		{
			StreamWriter sr = new StreamWriter(Path.Combine(Param.Logs, FileName), true);
			line = DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss - ") + line;
			sr.WriteLine(line);
			sr.Close();
		}
	}
}
