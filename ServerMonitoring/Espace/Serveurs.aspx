<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Serveurs.aspx.cs" Inherits="Espace_Serveurs" MasterPageFile="~/MP/MP.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
	<link rel="stylesheet" type="text/css" href="../CSS/serveur.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
	<div class="content">
		<div class="section">
			<div class="section-title">
				Serveurs
			</div>
			<asp:UpdatePanel runat="server" ID="upListeServeurs" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="section-content">
						<asp:Repeater runat="server" ID="rptServeurs">
							<ItemTemplate>
								<div class="serveur-container">
									<asp:LinkButton runat="server" CssClass='<%# "serveur" %>' CommandArgument='<%# Eval("id") %>' OnClick="btnServeur_Click">
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