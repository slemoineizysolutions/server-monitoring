using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServerMonitoring_fw;
using ServerMonitoring_fw.BIZ;

public partial class _Default : BasePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			MySession.LogOut();
		}
	}
	protected void btnConnecter_Click(object sender, EventArgs e)
	{
		string login = tbLogin.Text.Trim();
		string password = tbPassword.Text;
		if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
		{
			Utilisateur myUtilisateur = UtilisateurManager.Login(login, password);
			if (myUtilisateur != null)
			{
				//reussite
				MySession.SetSession("user", myUtilisateur);
				Session["GridviewUserIdKey"] = myUtilisateur.id;

				Response.Redirect("~/Espace/Default.aspx" + MySession.GenerateGetParams());
			}
			else
			{
				lblError.Text = "Mauvais login/mot de passe";

			}
		}
		else
		{
			lblError.Text = "Veuillez indiquer votre identifiant et votre mot de passe";
		}
	}
}