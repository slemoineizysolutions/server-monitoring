using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServerMonitoring_fw;

public partial class Espace_Serveurs : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Utilisateur myUser = GetUtilisateur();
        if (myUser != null)
        {
            if (!IsPostBack)
            {
                Initialization();
            }
        }
        else
            Response.Redirect("~/Default.aspx");
    }

    public void Initialization()
    {
        List<Serveur> listeServeurs = ServeurManager.FindAll();

        rptServeurs.DataSource = listeServeurs.OrderBy(s => s.libelle);
        rptServeurs.DataBind();
        upListeServeurs.Update();
    }

    protected void btnServeur_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        if (btn != null && !string.IsNullOrEmpty(btn.CommandArgument))
        {
            Response.Redirect("~/Espace/Serveur.aspx" + MySession.GenerateGetParams("id=" + btn.CommandArgument));
        }
    }
}