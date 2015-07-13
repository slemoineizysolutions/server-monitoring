using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServerMonitoring_fw;
using ServerMonitoring_fw.DAL;

namespace ServerMonitoring_fw
{
    public partial class UtilisateurManager
    {
        #region LOAD / FIND

        public static Utilisateur Load(int id)
        {
            return UtilisateurDB.Load(id);
        }

        public static List<Utilisateur> FindAll()
        {
            return UtilisateurDB.FindAll();
        }
		
		#endregion LOAD / FIND

        #region INSERT / UPDATE / DELETE

        public static Utilisateur Insert(Utilisateur myItem)
        {
            return UtilisateurDB.Insert(myItem);
        }

        public static Utilisateur Update(Utilisateur myItem)
        {
            return UtilisateurDB.Update(myItem);
        }

        public static bool Delete(int id)
        {
            return UtilisateurDB.Delete(id);
        }

        #endregion INSERT / UPDATE / DELETE
	}
}
