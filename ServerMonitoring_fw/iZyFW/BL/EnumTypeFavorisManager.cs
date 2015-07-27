using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServerMonitoring_fw;
using ServerMonitoring_fw.DAL;

namespace ServerMonitoring_fw
{
    public partial class EnumTypeFavorisManager
    {
        #region LOAD / FIND

        public static EnumTypeFavoris Load(int id)
        {
            return EnumTypeFavorisDB.Load(id);
        }

        public static List<EnumTypeFavoris> FindAll()
        {
            return EnumTypeFavorisDB.FindAll();
        }
		
		#endregion LOAD / FIND

        #region INSERT / UPDATE / DELETE

        public static EnumTypeFavoris Insert(EnumTypeFavoris myItem)
        {
            return EnumTypeFavorisDB.Insert(myItem);
        }

        public static EnumTypeFavoris Update(EnumTypeFavoris myItem)
        {
            return EnumTypeFavorisDB.Update(myItem);
        }

        public static bool Delete(int id)
        {
            return EnumTypeFavorisDB.Delete(id);
        }

        #endregion INSERT / UPDATE / DELETE
	}
}
