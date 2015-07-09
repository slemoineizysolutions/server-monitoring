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
				&nbsp;
				<asp:LinkButton runat="server" CssClass="log-add">
					<i class="fa fa-plus add"></i>
				</asp:LinkButton>
			</div>
			<asp:UpdatePanel runat="server" ID="upLogs" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="section-content">

						<%--<asp:Repeater runat="server" ID="rptLogs">
							<ItemTemplate>
								
								<div class="log">
									<asp:Label runat="server"
								</div>

							</ItemTemplate>
						</asp:Repeater>--%>

						<div class="log blue">
							<asp:Label runat="server" Text="Activa" CssClass="log-label-project"></asp:Label>
							-
							<asp:Label runat="server" Text="WebError" CssClass="log-label-subtitle"></asp:Label>

							<div class="log-buttons">
								<asp:LinkButton runat="server" CssClass="log-btn">
									<i class="fa fa-cogs"></i>
								</asp:LinkButton>
								<asp:LinkButton runat="server" CssClass="log-btn">
									<i class="fa fa-eye"></i>
								</asp:LinkButton>
								<asp:LinkButton runat="server" CssClass="log-btn">
									<i class="fa fa-floppy-o"></i>
								</asp:LinkButton>
							</div>
						</div>

						<div class="log blue">
							<asp:Label runat="server" Text="Activa" CssClass="log-label-project"></asp:Label>
							-
							<asp:Label runat="server" Text="iZyGridViewError" CssClass="log-label-subtitle"></asp:Label>

							<div class="log-buttons">
								<asp:LinkButton runat="server" CssClass="log-btn">
									<i class="fa fa-cogs"></i>
								</asp:LinkButton>
								<asp:LinkButton runat="server" CssClass="log-btn">
									<i class="fa fa-eye"></i>
								</asp:LinkButton>
								<asp:LinkButton runat="server" CssClass="log-btn">
									<i class="fa fa-floppy-o"></i>
								</asp:LinkButton>
							</div>
						</div>

						<div class="log orange">
							<asp:Label runat="server" Text="iZyFrais" CssClass="log-label-project"></asp:Label>
							-
							<asp:Label runat="server" Text="WebError" CssClass="log-label-subtitle"></asp:Label>

							<div class="log-buttons">
								<asp:LinkButton runat="server" CssClass="log-btn">
									<i class="fa fa-cogs"></i>
								</asp:LinkButton>
								<asp:LinkButton runat="server" CssClass="log-btn">
									<i class="fa fa-eye"></i>
								</asp:LinkButton>
								<asp:LinkButton runat="server" CssClass="log-btn">
									<i class="fa fa-floppy-o"></i>
								</asp:LinkButton>
							</div>
						</div>

						<div class="log red">
							<asp:Label runat="server" Text="iFaxnet" CssClass="log-label-project"></asp:Label>
							-
							<asp:Label runat="server" Text="WebError" CssClass="log-label-subtitle"></asp:Label>

							<div class="log-buttons">
								<asp:LinkButton runat="server" CssClass="log-btn">
									<i class="fa fa-cogs"></i>
								</asp:LinkButton>
								<asp:LinkButton runat="server" CssClass="log-btn">
									<i class="fa fa-eye"></i>
								</asp:LinkButton>
								<asp:LinkButton runat="server" CssClass="log-btn">
									<i class="fa fa-floppy-o"></i>
								</asp:LinkButton>
							</div>
						</div>

					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>

		<div class="section">
			<div class="section-title">
				Bases de données
			</div>
		</div>

	</div>
</asp:Content>
