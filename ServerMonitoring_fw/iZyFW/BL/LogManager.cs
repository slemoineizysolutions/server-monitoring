using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServerMonitoring_fw;
using ServerMonitoring_fw.DAL;

namespace ServerMonitoring_fw
{
    public partial class LogManager
    {
        #region LOAD / FIND

        public static Log Load(int id)
        {
            return LogDB.Load(id);
        }

        public static List<Log> FindAll()
        {
            return LogDB.FindAll();
        }
		
		#endregion LOAD / FIND

        #region INSERT / UPDATE / DELETE

        public static Log Insert(Log myItem)
        {
            return LogDB.Insert(myItem);
        }

        public static Log Update(Log myItem)
        {
            return LogDB.Update(myItem);
        }

        public static bool Delete(int id)
        {
            return LogDB.Delete(id);
        }

        #endregion INSERT / UPDATE / DELETE
	}
}
