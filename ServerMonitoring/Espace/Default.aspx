<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Espace_Default" MasterPageFile="~/MP/MP.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
	<link rel="stylesheet" type="text/css" href="../CSS/dashboard.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
	

	<div class="content">

		<div class="section">
			<div class="section-title">
				Informations du serveur
			</div>
			<div class="section-content">
				<table class="section-infos">
					<tr>
						<td>Nom du serveur</td>
						<td>
							<asp:Label runat="server" ID="lblNomServeur"></asp:Label>
						</td>
					</tr>
					<tr>
						<td>IP locale</td>
						<td>
							<asp:Label runat="server" ID="lblIPLocale"></asp:Label>
						</td>
					</tr>
					<tr>
						<td>IP publique</td>
						<td>
							<asp:Label runat="server" ID="lblIPPublique"></asp:Label>
						</td>
					</tr>
				</table>
			</div>
		</div>

		<div class="section">
			<div class="section-title">
				Performances
			</div>
			
			<asp:UpdatePanel runat="server" ID="upPerformances" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:Timer runat="server" ID="timerPerf" Interval="500" OnTick="timerPerf_Tick"></asp:Timer>
					<div class="section-content">

						<div class="perf-card cpu">
							<div class="perf-card-title">
								CPU
							</div>
							<div class="perf-card-valeur">
								<asp:Label runat="server" ID="lblCPUValeur"></asp:Label>
							</div>
						</div>

						<%--<div class="perf-card ram">
							<div class="perf-card-title">
								RAM
							</div>
							<div class="perf-card-valeur">
								<asp:Label runat="server" ID="lblRAMValeur"></asp:Label>
							</div>
						</div>--%>

						<div class="perf-card ram">
							<div class="perf-card-title">
								RAM Dispo
							</div>
							<div class="perf-card-valeur">
								<asp:Label runat="server" ID="lblRAMDispoValeur"></asp:Label>
							</div>
						</div>

						<div class="clear"></div>

					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>

		<div class="section">
			<div class="section-title">
				Logs
			</div>
		</div>

		<div class="section">
			<div class="section-title">
				Bases de données
			</div>
		</div>

	</div>
</asp:Content>
