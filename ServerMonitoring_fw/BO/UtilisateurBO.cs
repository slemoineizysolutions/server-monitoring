using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitoring_fw
{
	public partial class Utilisateur
	{
		
		private List<BaseDonnee> _myBaseDonneeFavoris
		{
			get;
			set;
		}
		private bool _myBaseDonneeFavorisLoad { get; set; }
		public List<BaseDonnee> myBaseDonneeFavoris
		{
			get
			{
				return BaseDonneeManager.FindFavoris(this.id);
			}
		}

		public List<Log> myLogFavoris
		{
			get
			{
				return LogManager.FindFavoris(this.id);

			}
		}
	}
}
