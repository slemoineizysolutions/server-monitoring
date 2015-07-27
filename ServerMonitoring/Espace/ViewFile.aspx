<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewFile.aspx.cs" Inherits="Espace_ViewFile" MasterPageFile="~/MP/MP.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
	<link rel="stylesheet" type="text/css" href="../CSS/viewfile.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
	<asp:Panel runat="server" ID="pnlHeader" CssClass="view-file-header">
		<asp:Label runat="server" ID="lblNomFichier" CssClass="view-file-header-libelle"></asp:Label>
		<br />
		<asp:Label runat="server" ID="lblCheminFichier" CssClass="view-file-header-chemin"></asp:Label>
		<br />
		<asp:Label runat="server" ID="lblCommentaire" CssClass="view-file-header-chemin"></asp:Label>
	</asp:Panel>
	<div class="header-space"></div>
	<pre>
<asp:Literal runat="server" ID="litFileContent"></asp:Literal>
	</pre>

</asp:Content>
