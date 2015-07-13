using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitoring_fw
{
	public partial class Log 
	{
	
	}

	public partial class CondLog
	{
		public Nullable<int> id { get; set; } 
		public Nullable<int> idProjet { get; set; } 
		public string libelle { get; set; } 
		public string cheminFichier { get; set; } 
	}
}
