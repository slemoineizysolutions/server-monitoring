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
				Gridview_Databind();
			}
		}
		else
			Response.Redirect("~/Default.aspx");
    }

	private void Gridview_Databind()
	{
		gvUsers.GenerateGridView(true, sortExp: "{nom}", sortDir: "ASC");
	}

	protected void btnEditUtilisateurAnnuler_Click(object sender, EventArgs e)
	{
		pnlEditUtilisateur.Visible = false;
		tbEditUtilisateurLogin.Text = string.Empty;
		tbEditUtilisateurNom.Text = string.Empty;
		tbEditUtilisateurPassword.Text = string.Empty;
		tbEditUtilisateurPasswordConfirmation.Text = string.Empty;
		hfUtilisateurId.Value = "";

		upGeneral.Update();
	}

	protected void btnEditUtilisateurSave_Click(object sender, EventArgs e)
	{
		bool isModif = false;
		Utilisateur user = new Utilisateur();
		if (!string.IsNullOrEmpty(hfUtilisateurId.Value))
		{
			user = UtilisateurManager.Load(iZyInt.ConvertStringToInt(hfUtilisateurId.Value));
			if (user != null) isModif = true;
			else user = new Utilisateur();
		}
		user.login = tbEditUtilisateurLogin.Text;
		user.nom = tbEditUtilisateurNom.Text;
		if (tbEditUtilisateurPassword.Text == tbEditUtilisateurPasswordConfirmation.Text && !string.IsNullOrEmpty(tbEditUtilisateurPasswordConfirmation.Text))
			user.password = tbEditUtilisateurPassword.Text;

		if (isModif) UtilisateurManager.Update(user);
		else UtilisateurManager.Insert(user);

		btnEditUtilisateurAnnuler_Click(null, null);
		Gridview_Databind();
	}

	protected void btnAddUtilisateur_Click(object sender, EventArgs e)
	{
		pnlEditUtilisateur.Visible = true;
		upGeneral.Update();
	}

	protected void btnEditUser_Click(object sender, EventArgs e)
	{
		LinkButton btn = (LinkButton)sender;
		if (btn != null && !string.IsNullOrEmpty(btn.CommandArgument))
		{
			int idUser = iZyInt.ConvertStringToInt(btn.CommandArgument);
			Utilisateur user = UtilisateurManager.Load(idUser);
			if (user != null)
			{
				tbEditUtilisateurLogin.Text = user.login;
				tbEditUtilisateurNom.Text = user.nom;
				hfUtilisateurId.Value = user.id.ToString();

				pnlEditUtilisateur.Visible = true;
				btnAddUtilisateur.Visible = false;

				upGeneral.Update();
			}
		}
	}

	protected void btnDeleteUser_Click(object sender, EventArgs e)
	{

	}
}