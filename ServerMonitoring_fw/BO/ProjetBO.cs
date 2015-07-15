using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitoring_fw
{
	public partial class Projet
	{
		private EnumTheme _myTheme
		{
			get;
			set;
		}
		private bool _myThemeLoad { get; set; }
		public EnumTheme myTheme
		{
			get
			{
				if (!_myThemeLoad)
				{
					_myTheme = EnumThemeManager.Load(this.idTheme);

					_myThemeLoad = true;
				}
				return _myTheme;
			}
			set
			{
				_myTheme = value;
				_myThemeLoad = true;
			}
		}
	}
}
