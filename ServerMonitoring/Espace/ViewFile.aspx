<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewFile.aspx.cs" Inherits="Espace_ViewFile" MasterPageFile="~/MP/MP.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" type="text/css" href="../CSS/viewfile.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <script>
        function ShowFileDelay() {
            setTimeout(ShowFile, 50);
        }

        function ShowFile() {
            $(".liste-fichier-contenu").addClass("cache");
            $(".fichier-contenu").addClass("visible");
            $(".btn-back").addClass("visible");
            console.log("showfile");
        }


        function HideFileDelay() {
            console.log("hide file delay");
            setTimeout(HideFile, 1000);
        }

        function HideFile() {
            console.log("hide file ");
            $(".liste-fichier-contenu").removeClass("cache");
            $(".fichier-contenu").removeClass("visible");
            $(".btn-back").removeClass("visible");
            console.log("hidefile");
        }

        function OpenCloseHeader() {
            $(".view-file-header").toggleClass("open");
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
        <asp:Image runat="server" ImageUrl="~/img/arrow-file.png" CssClass="header-arrow" onclick="OpenCloseHeader()" />
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

                        <asp:Repeater runat="server" ID="rptListeFolder">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btnGoFolder" CssClass="fichier" CommandArgument='<%# Eval("chemin") %>' OnClick="btnGoFolder_Click">
							        <i class="fa fa-folder-o fa-lg" title="Répertoire"></i>
							        <asp:Label runat="server" Text='<%# Eval("nom") %>'></asp:Label>
                                </asp:LinkButton>
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Repeater runat="server" ID="rptListeFichiers">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btnSeeFichier" CssClass="fichier" CommandArgument='<%# Eval("chemin") %>' OnClick="btnSeeFichier_Click">
                                    <i class="fa fa-file-text-o fa-lg" title="Fichier"></i>
                                    <asp:Label runat="server" Text='<%# Eval("nom") %>'></asp:Label>&nbsp;
							        <asp:Label runat="server" CssClass="fichier-size" Text='<%# Eval("taille") %>'></asp:Label>

                                </asp:LinkButton>
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>

                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel runat="server" ID="upFile" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnlContenuFichier" CssClass="fichier-contenu">
                        <pre>
<asp:Literal runat="server" ID="litFileContent"></asp:Literal>
	</pre>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
