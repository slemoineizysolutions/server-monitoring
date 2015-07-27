using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServerMonitoring_fw;

public partial class MP_MP : System.Web.UI.MasterPage
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
		Utilisateur user = (Utilisateur)MySession.GetSession("user");
		if (user != null)
		{
			if (!IsPostBack)
			{
				if (!string.IsNullOrEmpty(MySession.GetParam("menuDisabled")))
				{
					pnlMenuContainer.Visible = false;
				}
				else
				{
					lblUsernameConnecte.Text = user.nom;
					MenuInit();
				}
			}
		}
	}

	protected void MenuInit()
	{
		hlMenuMonCompte.NavigateUrl = "~/Espace/Default.aspx" + MySession.GenerateGetParams();
		hlMenuDashboard.NavigateUrl = "~/Espace/Default.aspx" + MySession.GenerateGetParams();
		hlMenuServeurs.NavigateUrl = "~/Espace/Default.aspx" + MySession.GenerateGetParams();
		hlMenuProjets.NavigateUrl = "~/Espace/Projets.aspx" + MySession.GenerateGetParams();
		hlMenuUtilisateurs.NavigateUrl = "~/Espace/Utilisateurs.aspx" + MySession.GenerateGetParams();
	}
	protected void btnDeconnexion_Click(object sender, EventArgs e)
	{
		MySession.LogOut();
		Response.Redirect("~/Default.aspx" + MySession.GenerateGetParams());
	}
}
