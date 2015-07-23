using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

using ServerMonitoring_fw.DAL;
using ServerMonitoring_fw.BIZ;
using System.Diagnostics;

namespace ServerMonitoring_fw
{
	public partial class BaseDonneeManager
	{
		public static string Sauvegarde(BaseDonnee myDatabase, bool download = false)
		{
			string cheminFile = string.Empty;
			if (File.Exists(Param.MysqlBinPath) && myDatabase != null)
			{
				string repPath = string.Empty;
				if (download)
					repPath = Param.TMP;
				else
					repPath = myDatabase.cheminSauvegarde;

				cheminFile = Path.Combine(repPath, "bdd-" + myDatabase.databaseName + "-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".sql");

				//string args = " -h " + myDatabase.host + " -u " + myDatabase.user + " -p" + myDatabase.password + " -r \"" + cheminFile + "\" " + myDatabase.databaseName;
				//Process.Start(Param.MysqlBinPath, args);



				//On lance le processus 
				ProcessStartInfo myProcessStartInfo = new ProcessStartInfo();
				myProcessStartInfo.FileName = "cmd";
				myProcessStartInfo.Arguments = "/c \"\"" + Param.MysqlBinPath + "\" -h " + myDatabase.host + " -u " + myDatabase.user + " -p" + myDatabase.password + " --quick -B " + myDatabase.databaseName + "  > \"" + cheminFile + "\"\"";
				myProcessStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				Process myProcess = new Process();
				myProcess.StartInfo = myProcessStartInfo;
				myProcess.Start();
				myProcess.WaitForExit();

				if (File.Exists(cheminFile))
				{
					string zipfile = Path.Combine(repPath, Path.GetFileNameWithoutExtension(cheminFile) + ".zip");
					if (File.Exists(zipfile))
					{
						File.Delete(zipfile);
					}

					using (ZipArchive zip = ZipFile.Open(zipfile, ZipArchiveMode.Create))
					{
						zip.CreateEntryFromFile(cheminFile, Path.GetFileName(cheminFile));
						File.Delete(cheminFile);
						cheminFile = Path.Combine(repPath, zipfile);
					}
				}
			}
			return cheminFile;
		}
	}
}
