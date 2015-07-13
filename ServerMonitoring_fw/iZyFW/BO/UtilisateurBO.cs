using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitoring_fw
{
	public partial class Utilisateur 
	{
	
	}

	public partial class CondUtilisateur
	{
		public Nullable<int> id { get; set; } 
		public string nom { get; set; } 
		public string login { get; set; } 
		public string password { get; set; } 
	}
}
