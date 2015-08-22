using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServerMonitoring_fw;

public partial class Espace_Projets : BasePage
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
		List<Projet> listeProjet = ProjetManager.FindAll();

		rptProjets.DataSource = listeProjet.OrderBy(p => p.libelle);
		rptProjets.DataBind();
		upListeProjets.Update();
	}

	protected void btnProjet_Click(object sender, EventArgs e)
	{
		LinkButton btn = (LinkButton)sender;
		if (btn != null && !string.IsNullOrEmpty(btn.CommandArgument))
		{
			Response.Redirect("~/Espace/Projet.aspx" + MySession.GenerateGetParams("id=" + btn.CommandArgument));
		}
	} 
}