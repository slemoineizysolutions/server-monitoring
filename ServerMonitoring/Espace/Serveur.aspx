<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Serveur.aspx.cs" Inherits="Espace_Serveur" MasterPageFile="~/MP/MP.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="iZyWebServerControl" Namespace="iZyWebServerControl" TagPrefix="iZy" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
	<link rel="stylesheet" type="text/css" href="../CSS/serveur.css" />
	<link rel="stylesheet" type="text/css" href="../CSS/dashboard.css" />
	<link rel="stylesheet" type="text/css" href="../CSS/circle.css" />
	<script src="/js/Chart.min.js"></script>
</asp:Content>


<asp:Content runat="server" ContentPlaceHolderID="Content">
	<div class="content">
		<asp:UpdatePanel runat="server" ID="upGeneral" UpdateMode="Conditional">
			<ContentTemplate>

				<asp:Panel runat="server" ID="pnlPageTitle" CssClass="page-title">
					<asp:HyperLink runat="server" CssClass="back" ID="hlBackProjet" NavigateUrl="~/Espace/Serveurs.aspx">
						<i class="fa fa-arrow-circle-left fa-lg"></i>
					</asp:HyperLink>

					<asp:Label runat="server" ID="projetName"></asp:Label>
				</asp:Panel>

				<div class="section">
					<div class="section-title">
						Infos générales
					</div>
					<asp:UpdatePanel runat="server" ID="upInfos" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Panel runat="server" DefaultButton="btnEditServeur" class="section-content">
								<table class="table-infos">
									<tbody>
										<tr>
											<td>Nom<br />
												<asp:TextBox runat="server" ID="tbNomServeur" CssClass="form-control" placeholder="Nom du serveur"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td>IP locale<br />
												<asp:TextBox runat="server" ID="tbIPLocale" CssClass="form-control" placeholder="IP Locale du serveur"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td>IP publique<br />
												<asp:TextBox runat="server" ID="tbIPPublique" CssClass="form-control" placeholder="IP Publique du serveur"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td>Chemin de l'executable ServerInfosMonitoring<br />
												<asp:TextBox runat="server" ID="tbCheminServerInfosExe" CssClass="form-control" placeholder="Chemin de l'executable"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td>
												<br />
												<asp:Button runat="server" ID="btnEditServeur" Text="Sauvegarder" CssClass="btn btn-block btn-lg btn-info" OnClick="btnEditServeur_Click" />
												<div style="text-align: center">
													<asp:Label runat="server" ID="lblMessageSauvegarde" CssClass="serveur-save-message"></asp:Label>
												</div>
											</td>
										</tr>
								</table>


							</asp:Panel>
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>

				<div class="section">
					<div class="section-title">
						Monitoring
					</div>
					<asp:Timer runat="server" ID="timerMonitoring" OnTick="timerMonitoring_Tick" Interval="1000"></asp:Timer>
					<asp:UpdatePanel runat="server" ID="upMonitoring" UpdateMode="Conditional">
						<ContentTemplate>
							<table class="table-monitoring">
								<tbody>
									<tr>
										<td class="td30">
											<div class="monitoring-title">
												CPU
											</div>
											<asp:Panel runat="server" ID="pnlCPuCircle" CssClass="c100 p50 big">
												<asp:Label runat="server" ID="lblCPUValue">50%</asp:Label>
												<div class="slice">
													<div class="bar"></div>
													<div class="fill"></div>
												</div>
											</asp:Panel>
										</td>
										<td class="td30">
											<div class="monitoring-title">
												Mémoire disponible
											</div>
											<asp:Panel runat="server" ID="pnlRAMCircle" CssClass="c100 p50 big">
												<asp:Label runat="server" ID="lblRAMValue">50%</asp:Label>
												<div class="slice">
													<div class="bar"></div>
													<div class="fill"></div>
												</div>
											</asp:Panel>
										</td>
										<td class="td40">
											<div class="monitoring-title">
												Espace disque libre
											</div>
											<asp:Literal runat="server" ID="litStockages"></asp:Literal>
											
										</td>
									</tr>
								</tbody>
							</table>
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>

			</ContentTemplate>
		</asp:UpdatePanel>
	</div>
</asp:Content>
