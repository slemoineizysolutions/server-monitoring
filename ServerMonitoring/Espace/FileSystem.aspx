<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileSystem.aspx.cs" Inherits="Espace_FileSystem" MasterPageFile="~/MP/MP.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" type="text/css" href="../CSS/viewfile.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <script>
        function ClosePopup() {
            $(".popup-fichier-contenu-background, .popup-fichier-contenu").removeClass("popup-visible");
        }

        function SearchInFile() {
            var searchText = $("#searchFile").val();
            $(".fichier-table tr").show();
            if (searchText != "")
                $(".fichier-table tr:not(:contains('" + searchText + "'))").fadeOut(300);
        }

        function MarkInFile() {
            var searchText = $("#searchFile").val();
            $(".fichier-table tr").removeClass("text-marked");
            if (searchText != "")
                $(".fichier-table tr:contains('" + searchText + "')").addClass("text-marked");
        }
    </script>

    <asp:Panel runat="server" ID="pnlHeader" CssClass="view-file-header">
        <asp:LinkButton runat="server" CssClass="btn-back" OnClick="btnGoBackListe_Click">
			<i class="fa fa-arrow-circle-left fa-2x"></i>
        </asp:LinkButton>

        <asp:Label runat="server" ID="lblNomFichier" CssClass="view-file-header-libelle"></asp:Label>
        &nbsp;
		<i runat="server" id="icon_folder" visible="false" class="fa fa-folder-o fa-lg" title="Répertoire"></i>
        <i runat="server" id="icon_file" visible="false" class="fa fa-file-text-o fa-lg" title="Fichier"></i>
        <br />
        <asp:Label runat="server" ID="lblCheminFichier" CssClass="view-file-header-chemin"></asp:Label>
        <br />
        <asp:Label runat="server" ID="lblCommentaire" CssClass="view-file-header-chemin"></asp:Label>


    </asp:Panel>
    <div class="header-space"></div>

    <asp:UpdatePanel runat="server" ID="upGeneral" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:UpdatePanel runat="server" ID="upListes" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnlListeFichier" CssClass="liste-fichier-contenu">

                        <table>
                            <tbody>

                                <asp:Repeater runat="server" ID="rptListeFolder">
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="3">
                                                <asp:LinkButton runat="server" ID="btnGoFolder" CssClass="fichier" ClientIDMode="AutoID" CommandArgument='<%# Eval("chemin") %>' OnClick="btnGoFolder_Click">
							                        <i class="fa fa-folder-o fa-lg" title="Répertoire"></i>
							                        <asp:Label runat="server" Text='<%# Eval("nom") %>'></asp:Label>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>

                                    </ItemTemplate>
                                </asp:Repeater>

                                <asp:Repeater runat="server" ID="rptListeFichiers">
                                    <ItemTemplate>
                                        <tr class="fichier">
                                            <td>
                                                <i class="fa fa-file-text-o fa-lg" title="Fichier"></i>
                                                <asp:Label runat="server" Text='<%# Eval("nom") %>'></asp:Label>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label runat="server" CssClass="fichier-size" Text='<%# Eval("taille") %>'></asp:Label>
                                            </td>
                                            <td class="fichier-actions">
                                                <asp:LinkButton runat="server" ID="btnSeeFichier" ClientIDMode="AutoID" CommandArgument='<%# Eval("chemin") %>' OnClick="btnSeeFichier_Click" ToolTip="Voir le fichier">
                                                    <i class="fa fa-eye"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="btnDownloadFichier" ClientIDMode="AutoID" CommandArgument='<%# Eval("chemin") %>' OnClick="btnDownloadFichier_Click" ToolTip="Télécharger le fichier">
                                                    <i class="fa fa-download"></i>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel runat="server" ID="upFile" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnlPopupFichierBackground" CssClass="popup-fichier-contenu-background"></asp:Panel>
                    <asp:Panel runat="server" ID="pnlContenuFichier" CssClass="popup-fichier-contenu">
                        <asp:Panel runat="server" ID="pnlContenuFichierHeader" CssClass="popup-fichier-header">
                            <asp:Label runat="server" ID="lblFichierChemin" CssClass="popup-fichier-nom"></asp:Label>

                            <span class="popup-fichier-search">
                                <i class="fa fa-search"></i>
                                <input type="text" id="searchFile" />
                            </span>
                            <a href="javascript:SearchInFile()" class="file-search-action">Rechercher</a>
                            <a href="javascript:MarkInFile()" class="file-search-action">Marquer</a>

                            <span class="popup-close" onclick="ClosePopup()">X</span>
                        </asp:Panel>
                        <table class="fichier-table">
                            <asp:Literal runat="server" ID="litFileContent"></asp:Literal>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
