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
			</div>

			<asp:UpdatePanel runat="server" ID="upGeneral" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="section-content">
						<br />
						<iZy:IZyGrid ID="gvUsers" runat="server" Width="100%" TableName="Utilisateur" AllowSorting="true" CssClass="table-data" AllowPaging="true"
							AllowExportCSV="true" AllowUserChooseColumns="true" AllowUserChangeHeaderOrder="true" AllowUserChangeHeaderSize="true" PageSizeValues="5;10;20"
							AllowSavingParameters="true">
							<Columns>
								<iZy:BoundField DataField="Nom" HeaderText="Nom" />

								<iZy:BoundField DataField="login" HeaderText="Email" />

								<iZy:TemplateField HeaderText="" PreventHideColumn="true">
									<ItemTemplate>
										<asp:LinkButton ID="btnEditUser" runat="server" Font-Size="24" Width="30" CommandArgument='<%# Eval("Id") %>'>
											<i class="fa fa-cog"></i>
										</asp:LinkButton>
									</ItemTemplate>
								</iZy:TemplateField>
								<iZy:TemplateField HeaderText="" PreventHideColumn="true">
									<ItemTemplate>
										<asp:LinkButton ID="btnDeleteUser" runat="server" Font-Size="24" Width="30" CommandArgument='<%# Eval("Id") %>'>
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
