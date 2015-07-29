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
					lblCheminFichier.Text = myLog.cheminFichier;
					lblCommentaire.Text = myLog.commentaire;
					pnlHeader.CssClass += " " + myLog.myProjet.myTheme.cssClass;

					bool? isDirectory = IsDirectory(myLog.cheminFichier);
					if (isDirectory.HasValue)
					{
						if (isDirectory.Value) // c'est un répertoire
						{
							icon_folder.Visible = true;
							pnlListeFichier.Visible = true;

							pnlListeFichier.CssClass += " " + myLog.myProjet.myTheme.cssClass;
							DisplayListeFichiers(myLog.cheminFichier);
						}
						else // c'est un fichier
						{
							pnlListeFichier.Visible = false;
							pnlContenuFichier.CssClass += " visible";

							icon_file.Visible = true;
							DisplayFichierContenu(myLog.cheminFichier);
						}
					}
					else
					{
						litFileContent.Text = "Le fichier n'existe pas ou le chemin est inaccessible";
					}
				}
			}
		}
		else
			Response.Redirect("~/Default.aspx");
	}

	public bool? IsDirectory(string path)
	{
		if (Directory.Exists(path))
			return true; // is a directory 
		else if (File.Exists(path))
			return false; // is a file 
		else
			return null; // is a nothing 
	}

	public void DisplayFichierContenu(string cheminFichier)
	{
		if (File.Exists(cheminFichier))
			litFileContent.Text = File.ReadAllText(cheminFichier);
		else
			litFileContent.Text = "Le fichier n'existe pas ou le chemin est inaccessible";
	}



	public class Fichier
	{
		public string chemin { get; set; }
		public string nom { get; set; }
	}

	public class Repertoire
	{
		public string chemin { get; set; }
		public string nom { get; set; }
	}

	protected void DisplayListeFichiers(string cheminRepertoire)
	{
		if (Directory.Exists(cheminRepertoire))
		{
			string[] arrayFichiers = Directory.GetFiles(cheminRepertoire);

			List<Fichier> listeFichiers = arrayFichiers.Select(f => new Fichier() { chemin = f, nom = Path.GetFileName(f) }).ToList();
			rptListeFichiers.DataSource = listeFichiers.OrderBy(f => f.nom);
			rptListeFichiers.DataBind();


			string[] listeFolder = Directory.GetDirectories(cheminRepertoire);
			List<Repertoire> listeRepertoire = listeFolder.Select(f => new Repertoire() { chemin = f, nom = Path.GetFileName(f) }).ToList();
			rptListeFolder.DataSource = listeRepertoire.OrderBy(f => f.nom);
			rptListeFolder.DataBind();
		}
	}

	protected void btnSeeFichier_Click(object sender, EventArgs e)
	{
		LinkButton btn = (LinkButton)sender;
		if (btn != null && !string.IsNullOrEmpty(btn.CommandArgument))
		{
			DisplayFichierContenu(btn.CommandArgument);

			ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowFileDelay", "ShowFileDelay();", true);
			upFile.Update();
		}
	}
	protected void btnGoFolder_Click(object sender, EventArgs e)
	{

	}
	protected void btnGoBackListe_Click(object sender, EventArgs e)
	{
		int idLog = iZyInt.ConvertStringToInt(MySession.GetParam("idLog"));
		Log myLog = LogManager.Load(idLog);
		if (myLog != null)
		{
			DisplayListeFichiers(myLog.cheminFichier);
			ScriptManager.RegisterStartupScript(this, this.GetType(), "HideFileDelay", "HideFileDelay();", true);
			upGeneral.Update();
		}
	}
}