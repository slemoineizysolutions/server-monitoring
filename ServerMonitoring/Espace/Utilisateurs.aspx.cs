using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServerMonitoring_fw;
using ServerMonitoring_fw.BIZ;
using iZyTools.Convertion;

public partial class Espace_Utilisateurs : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
		Utilisateur myUser = GetUtilisateur();
		if (myUser != null)
		{
			if (!IsPostBack)
			{

				gvUsers.GenerateGridView(true, sortExp: "{nom}", sortDir: "ASC");
			}
		}
		else
			Response.Redirect("~/Default.aspx");
    }
}