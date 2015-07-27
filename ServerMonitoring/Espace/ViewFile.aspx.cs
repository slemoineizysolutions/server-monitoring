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
					if (File.Exists(myLog.cheminFichier))
						litFileContent.Text = File.ReadAllText(myLog.cheminFichier);
					else
						litFileContent.Text = "Le fichier n'existe pas ou le chemin est inaccessible";
					lblCheminFichier.Text = myLog.cheminFichier;
					pnlHeader.CssClass += " " + myLog.myProjet.myTheme.cssClass;
				}
			}
		}
		else
			Response.Redirect("~/Default.aspx");
	}
}