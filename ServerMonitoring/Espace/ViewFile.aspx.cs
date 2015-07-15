using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using ServerMonitoring_fw;
using iZyTools.Convertion;

public partial class Espace_ViewFile : BasePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		Utilisateur myUser = GetUtilisateur();
		if (myUser != null)
		{
			if (!IsPostBack)
			{
				int idLog = iZyInt.ConvertStringToInt(MySession.GetParam("idLog"));
				Log myLog = LogManager.Load(idLog);
				if (myLog != null)
				{
					lblNomFichier.Text = myLog.myProjet.libelle + " - " + myLog.libelle;
					litFileContent.Text = File.ReadAllText(myLog.cheminFichier);
					lblCheminFichier.Text = myLog.cheminFichier;
					pnlHeader.CssClass += " " + myLog.myProjet.myTheme.cssClass;
				}
			}
		}
		else
			Response.Redirect("~/Default.aspx");
	}
}