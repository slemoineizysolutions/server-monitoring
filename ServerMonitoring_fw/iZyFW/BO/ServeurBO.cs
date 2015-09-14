using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitoring_fw
{
	public partial class Serveur 
	{
	
	}

	public partial class CondServeur
	{
		public Nullable<int> id { get; set; } 
		public string libelle { get; set; } 
		public string ipLocale { get; set; }
		public string ipPublique { get; set; }
		public string cheminInfosMonitoring { get; set; } 
	}
}
