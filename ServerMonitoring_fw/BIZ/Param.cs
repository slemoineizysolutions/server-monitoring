using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitoring_fw.BIZ
{
	public class Param
	{
		#region General
		private static string GetSingleParameter(string name)
		{
			List<string> res = getParameter(name);
			if (res != null)
				return res.First();
			else
				return string.Empty;
		}

		private static List<string> GetMultiParameter(string name)
		{
			List<string> res = getParameter(name);
			if (res != null)
				return res;
			else
				return new List<string>();
		}

		public static List<string> getParameter(string parameter)
		{
			List<string> retour = null;
			string fileName = Path.Combine(Path.Combine(ServerMonitoringScriptsPath, "params.ini"));
			if (File.Exists(fileName))
			{
				StreamReader sr = new StreamReader(fileName);
				string line = "";
				while (sr.Peek() > 0)
				{
					line = sr.ReadLine();
					if (line.Trim().Length > 0)
					{
						string[] token = line.Split(new char[] { '=' });
						if (token.Length > 1)
						{
							if (token[0] == parameter)
							{
								retour = new List<string>();
								string[] values = token[1].Split(new char[] { ';' });
								for (int i = 0; i < values.Length; i++)
								{
									retour.Add(values[i]);
								}
								sr.Close();
								return retour;
							}
						}
					}
				}
				sr.Close();
			}
			else
			{
			}
			return retour;
		}
		#endregion General

		#region DataBase

		public static string MySQLConnectionString
		{
			get
			{
				return "server=" + MySQLHost + ";user id=" + MySQLUser + ";password=" + MySQLPassword + ";persist security info=True;database=" + MySQLDatabase + ";allow user variables=true;Convert Zero Datetime=True";
			}
		}

		public static string MySQLHost
		{
			get
			{
				List<string> res = getParameter("mysql.host");
				if (res != null)
					return res.First();
				else
					return string.Empty;
			}
		}

		public static string MySQLUser
		{
			get
			{
				List<string> res = getParameter("mysql.user");
				if (res != null)
					return res.First();
				else
					return string.Empty;
			}
		}

		public static string MySQLPassword
		{
			get
			{
				List<string> res = getParameter("mysql.password");
				if (res != null)
					return res.First();
				else
					return string.Empty;
			}
		}

		public static string MySQLDatabase
		{
			get
			{
				List<string> res = getParameter("mysql.database");
				if (res != null)
					return res.First();
				else
					return string.Empty;
			}
		}

		#endregion DataBase


		public static string ServerMonitoringPath
		{
			get { return Environment.GetEnvironmentVariable("SERVERMONITORING"); }
		}

		public static string ServerMonitoringScriptsPath
		{
			get { return Path.Combine(ServerMonitoringPath, "scripts"); }
		}

	

		#region URL

		public static string ConfirmationMDPURL
		{
			get
			{
				return GetSingleParameter("url.confirmationmdp");
			}
		}


		#endregion URL

		

		
	}
}
