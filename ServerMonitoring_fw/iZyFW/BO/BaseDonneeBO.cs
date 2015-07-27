using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitoring_fw
{
	public partial class BaseDonnee 
	{
	
	}

	public partial class CondBaseDonnee
	{
		public Nullable<int> id { get; set; } 
		public Nullable<int> idProjet { get; set; } 
		public string libelle { get; set; } 
		public string host { get; set; } 
		public string databaseName { get; set; } 
		public string user { get; set; } 
		public string password { get; set; } 
		public string cheminSauvegarde { get; set; } 
	}
}
