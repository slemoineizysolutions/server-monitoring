using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServerMonitoring_fw;
using ServerMonitoring_fw.DAL;

namespace ServerMonitoring_fw
{
    public partial class BaseDonneeManager
    {
        #region LOAD / FIND

        public static BaseDonnee Load(int id)
        {
            return BaseDonneeDB.Load(id);
        }

        public static List<BaseDonnee> FindAll()
        {
            return BaseDonneeDB.FindAll();
        }
		
		#endregion LOAD / FIND

        #region INSERT / UPDATE / DELETE

        public static BaseDonnee Insert(BaseDonnee myItem)
        {
            return BaseDonneeDB.Insert(myItem);
        }

        public static BaseDonnee Update(BaseDonnee myItem)
        {
            return BaseDonneeDB.Update(myItem);
        }

        public static bool Delete(int id)
        {
            return BaseDonneeDB.Delete(id);
        }

        #endregion INSERT / UPDATE / DELETE
	}
}
