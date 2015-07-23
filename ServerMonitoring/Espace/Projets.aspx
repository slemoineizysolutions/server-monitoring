<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Projets.aspx.cs" Inherits="Espace_Projets" MasterPageFile="~/MP/MP.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
	<link rel="stylesheet" type="text/css" href="../CSS/projet.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
	<div class="content">
		<div class="section">
			<div class="section-title">
				Projets
			</div>
			<asp:UpdatePanel runat="server" ID="upListeProjets" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="section-content">
						<asp:Repeater runat="server" ID="rptProjets">
							<ItemTemplate>
								<div class="projet-container">
									<asp:LinkButton runat="server" CssClass='<%# "projet "+Eval("myTheme.CssClass") %>' CommandArgument='<%# Eval("id") %>' OnClick="btnProjet_Click">
										<asp:Label runat="server" Text='<%# Eval("libelle") %>'></asp:Label>
									</asp:LinkButton>
								</div>
							</ItemTemplate>
						</asp:Repeater>

					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>

	</div>
</asp:Content>
