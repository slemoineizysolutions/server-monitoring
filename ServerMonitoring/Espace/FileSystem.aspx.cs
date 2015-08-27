using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO.Compression;
using System.IO;
using System.Text.RegularExpressions;

using ServerMonitoring_fw;
using ServerMonitoring_fw.BIZ;
using iZyTools.Convertion;

public partial class Espace_FileSystem : BasePage
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
                    pnlContenuFichierHeader.CssClass += " " + myLog.myProjet.myTheme.cssClass;

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

                    }
                    else
                    {
                        litFileContent.Text = "Le répertoire n'existe pas ou est inaccessible";
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



    public class Fichier
    {
        public string chemin { get; set; }
        public string nom { get; set; }
        public string taille { get; set; }
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
            DirectoryInfo dir = new DirectoryInfo(cheminRepertoire);
            FileInfo[] arrayFichiers = dir.GetFiles();


            List<Fichier> listeFichiers = arrayFichiers.Select(f => new Fichier() { chemin = f.FullName, nom = f.Name, taille = ConvertOctets(f.Length) }).ToList();
            rptListeFichiers.DataSource = listeFichiers.OrderBy(f => f.nom);
            rptListeFichiers.DataBind();


            DirectoryInfo[] listeFolder = dir.GetDirectories();
            List<Repertoire> listeRepertoire = listeFolder.Select(d => new Repertoire() { chemin = d.FullName, nom = d.Name }).ToList();
            rptListeFolder.DataSource = listeRepertoire.OrderBy(f => f.nom);
            rptListeFolder.DataBind();
        }
    }

    protected void btnSeeFichier_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        if (btn != null && !string.IsNullOrEmpty(btn.CommandArgument))
        {
            DisplayPopupFichierContenu(btn.CommandArgument);
            //upListes.Update();
        }
    }

    protected void btnDownloadFichier_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        if (btn != null && !string.IsNullOrEmpty(btn.CommandArgument))
        {
            string filePath = btn.CommandArgument;
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            if (File.Exists(filePath))
            {
                string zipfile = Path.Combine(Param.TMP, fileName + ".zip");
                if (File.Exists(zipfile))
                {
                    File.Delete(zipfile);
                }

                using (ZipArchive zip = ZipFile.Open(zipfile, ZipArchiveMode.Create))
                {
                    zip.CreateEntryFromFile(filePath, fileName + ".log");
                }

                if (File.Exists(zipfile))
                {
                    Response.AppendHeader("content-disposition", "attachment; filename=" + fileName + ".zip");
                    Response.ContentType = "application/zip";
                    Response.WriteFile(zipfile);
                }
            }
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

    protected void DisplayPopupFichierContenu(string cheminFichier)
    {
        if (!string.IsNullOrEmpty(cheminFichier) && File.Exists(cheminFichier))
        {
            lblFichierChemin.Text = Path.GetFileName(cheminFichier);

            List<string> fichierLignes = File.ReadAllLines(cheminFichier).ToList();

            for (int i = 0; i < fichierLignes.Count; i++)
            {
                
                // ^[0-9]{2}/[0-9]{2}/[0-9]{4}-[0-9]{2}:[0-9]{2}:[0-9]{2}
                Regex reg = new Regex("^[0-9]{2}/[0-9]{2}/[0-9]{4}-[0-9]{2}:[0-9]{2}:[0-9]{2}");
                Match m = reg.Match(fichierLignes[i]);
                if (m.Success)
                {
                    string date = m.Value;
                    string dateFormat = "<b>" + date + "</b>";
                    fichierLignes[i] = fichierLignes[i].Replace(date, dateFormat);
                }
                fichierLignes[i] = "<tr><td><span class='fichier-contenu-line-number'>" + (i + 1) + "</span></td><td>" + fichierLignes[i] + "</td></tr>";
                //
            }


            litFileContent.Text = string.Join(Environment.NewLine, fichierLignes);

            pnlPopupFichierBackground.CssClass += " popup-visible";
            pnlContenuFichier.CssClass += " popup-visible";

            upFile.Update();
        }
    }

    #region TOOLS
    protected string ConvertOctets(long octets)
    {
        string res = string.Empty;
        long L_Size = octets;

        if (L_Size / 1024 > 1)
        {
            L_Size = L_Size / 1024;
            if (L_Size / 1024 > 1)
            {
                L_Size = L_Size / 1024;
                if (L_Size / 1024 > 1)
                {
                    L_Size = L_Size / 1024;
                    if (L_Size / 1024 > 1)
                    {
                        L_Size = L_Size / 1024;
                    }
                    else
                    {
                        res = L_Size + " Go";
                    }
                }
                else
                {
                    res = L_Size + " Mo";
                }

            }
            else
            {
                res = L_Size + " Ko";
            }
        }
        else
        {
            res = L_Size + " Oct";
        }
        return res;
    }

    #endregion


}