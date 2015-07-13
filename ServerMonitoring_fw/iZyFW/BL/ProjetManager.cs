using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServerMonitoring_fw;
using ServerMonitoring_fw.DAL;

namespace ServerMonitoring_fw
{
    public partial class ProjetManager
    {
        #region LOAD / FIND

        public static Projet Load(int id)
        {
            return ProjetDB.Load(id);
        }

        public static List<Projet> FindAll()
        {
            return ProjetDB.FindAll();
        }
		
		#endregion LOAD / FIND

        #region INSERT / UPDATE / DELETE

        public static Projet Insert(Projet myItem)
        {
            return ProjetDB.Insert(myItem);
        }

        public static Projet Update(Projet myItem)
        {
            return ProjetDB.Update(myItem);
        }

        public static bool Delete(int id)
        {
            return ProjetDB.Delete(id);
        }

        #endregion INSERT / UPDATE / DELETE
	}
}
