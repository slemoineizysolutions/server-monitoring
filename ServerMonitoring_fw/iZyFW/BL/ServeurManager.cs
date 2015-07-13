using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServerMonitoring_fw;
using ServerMonitoring_fw.DAL;

namespace ServerMonitoring_fw
{
    public partial class ServeurManager
    {
        #region LOAD / FIND

        public static Serveur Load(int id)
        {
            return ServeurDB.Load(id);
        }

        public static List<Serveur> FindAll()
        {
            return ServeurDB.FindAll();
        }
		
		#endregion LOAD / FIND

        #region INSERT / UPDATE / DELETE

        public static Serveur Insert(Serveur myItem)
        {
            return ServeurDB.Insert(myItem);
        }

        public static Serveur Update(Serveur myItem)
        {
            return ServeurDB.Update(myItem);
        }

        public static bool Delete(int id)
        {
            return ServeurDB.Delete(id);
        }

        #endregion INSERT / UPDATE / DELETE
	}
}
