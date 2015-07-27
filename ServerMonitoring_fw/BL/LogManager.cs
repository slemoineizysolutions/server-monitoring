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
	public partial class LogManager
	{
		public static List<Log> FindAll(int idProjet)
		{
			return LogDB.FindAll(idProjet);
		}

		public static List<Log> FindFavoris(int idUtilisateur)
		{
			return LogDB.FindFavoris(idUtilisateur);
		}
	}
}
