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
	protected void Page_Init(object sender, EventArgs e)
	{

		if (Request.Params.Count == 0)
			Response.Redirect("~/Default.aspx");

		string guid = null;

		if (Request.Params["oldGUID"] != null && Request.Params["newGUID"] != null)
		{
			string oldGUID = Request.Params["oldGUID"].ToString();
			guid = Request.Params["newGUID"].ToString();
			MySession.DuplicateSession(oldGUID, guid);
			SESSIONID.Value = guid;
			string parametres = MySession.GetParams();

			Uri myUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
			string redirection = "";
			if (parametres != null && parametres.Length > 0)
				redirection = myUri.GetLeftPart(UriPartial.Path) + MySession.GenerateGetParamsWithNewGUID(parametres, guid);
			else
				redirection = myUri.GetLeftPart(UriPartial.Path) + MySession.GenerateGetParamsWithNewGUID(guid);

			Response.Redirect(redirection);
		}
		else
		{
			if (guid == null || guid == string.Empty)
				guid = Request.Form["ctl00$SESSIONID"];
			if (guid == null || guid == string.Empty)
				guid = MySession.GetParam("SESSIONID");
			if (guid != null)
				SESSIONID.Value = guid;
		}

	}

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