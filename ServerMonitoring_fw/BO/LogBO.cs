using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitoring_fw
{
	public partial class Log
	{
		private Projet _myProjet
		{
			get;
			set;
		}
		private bool _myProjetLoad { get; set; }
		public Projet myProjet
		{
			get
			{
				if (!_myProjetLoad)
				{
					_myProjet = ProjetManager.Load(this.idProjet);

					_myProjetLoad = true;
				}
				return _myProjet;
			}
			set
			{
				_myProjet = value;
				_myProjetLoad = true;
			}
		}
	}
}
