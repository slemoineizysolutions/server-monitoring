using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServerMonitoring_fw;
using ServerMonitoring_fw.DAL;

namespace ServerMonitoring_fw
{
    public partial class EnumThemeManager
    {
        #region LOAD / FIND

        public static EnumTheme Load(int id)
        {
            return EnumThemeDB.Load(id);
        }

        public static List<EnumTheme> FindAll()
        {
            return EnumThemeDB.FindAll();
        }
		
		#endregion LOAD / FIND

        #region INSERT / UPDATE / DELETE

        public static EnumTheme Insert(EnumTheme myItem)
        {
            return EnumThemeDB.Insert(myItem);
        }

        public static EnumTheme Update(EnumTheme myItem)
        {
            return EnumThemeDB.Update(myItem);
        }

        public static bool Delete(int id)
        {
            return EnumThemeDB.Delete(id);
        }

        #endregion INSERT / UPDATE / DELETE
	}
}
