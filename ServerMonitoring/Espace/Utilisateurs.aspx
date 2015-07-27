<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Utilisateurs.aspx.cs" Inherits="Espace_Utilisateurs" MasterPageFile="~/MP/MP.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="iZyWebServerControl" Namespace="iZyWebServerControl" TagPrefix="iZy" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
	<link rel="stylesheet" type="text/css" href="../CSS/projet.css" />
	<link rel="stylesheet" type="text/css" href="../CSS/dashboard.css" />
	<link rel="stylesheet" type="text/css" href="../CSS/gridview.css" />
</asp:Content>


<asp:Content runat="server" ContentPlaceHolderID="Content">
	<div class="content">

		<div class="section">
			<div class="section-title">
				Utilisateurs
				&nbsp;
				<asp:LinkButton runat="server" CssClass="log-add" ID="btnAddUtilisateur" OnClick="btnAddUtilisateur_Click">
					<i class="fa fa-plus add"></i>
				</asp:LinkButton>
			</div>

			<asp:UpdatePanel runat="server" ID="upGeneral" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="section-content">

						<asp:Panel runat="server" ID="pnlEditUtilisateur" CssClass="edit-log" Visible="false">
							<div class="edit-log-title">
								<asp:Label runat="server" ID="lblEditLogTitle" Text="Ajouter un utilisateur"></asp:Label>
							</div>
							<div class="edit-log-content">
								<table>
									<tbody>
										<tr>
											<td>Nom
												<asp:TextBox runat="server" ID="tbEditUtilisateurNom" placeholder="Nom & prénom" CssClass="form-control"></asp:TextBox>
											</td>
											<td>Login
												<asp:TextBox runat="server" ID="tbEditUtilisateurLogin" placeholder="Login" CssClass="form-control"></asp:TextBox>
											</td>
											<td>
												<asp:Button runat="server" ID="btnEditUtilisateurAnnuler" Text="Annuler" CssClass="btn btn-block btn-lg btn-danger" OnClick="btnEditUtilisateurAnnuler_Click" />
											</td>
										</tr>
										<tr>
											<td>Mot de passe
												<asp:TextBox runat="server" ID="tbEditUtilisateurPassword" placeholder="Mot de passe" TextMode="Password" CssClass="form-control"></asp:TextBox>
											</td>
											<td>Confirmation
												<asp:TextBox runat="server" ID="tbEditUtilisateurPasswordConfirmation" placeholder="Confirmation de mot de passe" TextMode="Password" CssClass="form-control"></asp:TextBox>
											</td>
											<td>
												<asp:HiddenField runat="server" ID="hfUtilisateurId" Visible="false" />
												<asp:Button runat="server" ID="btnEditUtilisateurSave" Text="Sauvegarder" CssClass="btn btn-block btn-lg btn-info" OnClick="btnEditUtilisateurSave_Click" />
											</td>
										</tr>
									</tbody>
								</table>
							</div>
						</asp:Panel>

						<br />
						<iZy:IZyGrid ID="gvUsers" runat="server" Width="100%" TableName="Utilisateur" AllowSorting="true" CssClass="table-data" AllowPaging="true"
							AllowExportCSV="true" AllowUserChooseColumns="true" AllowUserChangeHeaderOrder="true" AllowUserChangeHeaderSize="true" PageSizeValues="5;10;20"
							AllowSavingParameters="true">
							<Columns>
								<iZy:BoundField DataField="Nom" HeaderText="Nom" />

								<iZy:BoundField DataField="login" HeaderText="Email" />

								<iZy:TemplateField HeaderText="" PreventHideColumn="true">
									<ItemTemplate>
										<asp:LinkButton ID="btnEditUser" runat="server" Font-Size="24" Width="30" CommandArgument='<%# Eval("Id") %>' OnClick="btnEditUser_Click">
											<i class="fa fa-cog"></i>
										</asp:LinkButton>
									</ItemTemplate>
								</iZy:TemplateField>
								<iZy:TemplateField HeaderText="" PreventHideColumn="true">
									<ItemTemplate>
										<asp:LinkButton ID="btnDeleteUser" runat="server" Font-Size="24" Width="30" CommandArgument='<%# Eval("Id") %>' OnClick="btnDeleteUser_Click">
											<i class="fa fa-trash-o"></i>
										</asp:LinkButton>
									</ItemTemplate>
								</iZy:TemplateField>
							</Columns>
						</iZy:IZyGrid>
					</div>
				</ContentTemplate>
			</asp:UpdatePanel>

		</div>
	</div>
</asp:Content>
