<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewFile.aspx.cs" Inherits="Espace_ViewFile" MasterPageFile="~/MP/MP.master" %>


<asp:Content runat="server" ContentPlaceHolderID="Content">
	<div class="view-file">
		<asp:Label runat="server" ID="lblNomFichier"></asp:Label>
		<pre>
		<asp:Literal runat="server" ID="litFileContent"></asp:Literal>
	</pre>
	</div>
</asp:Content>
