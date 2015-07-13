using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServerMonitoring_fw.DAL;

namespace ServerMonitoring_fw
{
	public partial class UtilisateurManager
	{
		public static Utilisateur Login(string login, string password)
		{
			return UtilisateurDB.Login(login, password);
		}
	}
}
