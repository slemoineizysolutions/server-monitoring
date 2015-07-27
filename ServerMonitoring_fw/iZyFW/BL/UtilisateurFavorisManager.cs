using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServerMonitoring_fw;
using ServerMonitoring_fw.DAL;

namespace ServerMonitoring_fw
{
    public partial class UtilisateurFavorisManager
    {
        #region LOAD / FIND

        public static UtilisateurFavoris Load(int idUtilisateur ,int idEntite ,int idType)
        {
            return UtilisateurFavorisDB.Load(idUtilisateur ,idEntite ,idType);
        }

        public static List<UtilisateurFavoris> FindAll()
        {
            return UtilisateurFavorisDB.FindAll();
        }
		
		#endregion LOAD / FIND

        #region INSERT / UPDATE / DELETE

        public static UtilisateurFavoris Insert(UtilisateurFavoris myItem)
        {
            return UtilisateurFavorisDB.Insert(myItem);
        }

        public static UtilisateurFavoris Update(UtilisateurFavoris myItem)
        {
            return UtilisateurFavorisDB.Update(myItem);
        }

        public static bool Delete(int idUtilisateur ,int idEntite ,int idType)
        {
            return UtilisateurFavorisDB.Delete(idUtilisateur ,idEntite ,idType);
        }

        #endregion INSERT / UPDATE / DELETE
	}
}
