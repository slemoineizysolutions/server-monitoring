using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitoring_fw
{
	public partial class Projet 
	{
	
	}

	public partial class CondProjet
	{
		public Nullable<int> id { get; set; } 
		public string libelle { get; set; } 
		public Nullable<int> idTheme { get; set; } 
		public string urlProd { get; set; } 
		public string urlTest { get; set; } 
	}
}
