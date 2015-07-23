<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Espace_Default" MasterPageFile="~/MP/MP.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
	<link rel="stylesheet" type="text/css" href="../CSS/dashboard.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">


	<div class="content">

		<div class="section">
			<div class="section-title">
				Serveur
			</div>
			<asp:UpdatePanel runat="server" ID="upPerformances" UpdateMode="Conditional">
				<ContentTemplate>
					<%--<asp:Timer runat="server" ID="timerPerf" Interval="1000" OnTick="timerPerf_Tick"></asp:Timer>--%>
					<div class="section-content">
						<table class="section-infos">
							<tr>
								<td>Nom du serveur</td>
								<td>
									<asp:Label runat="server" ID="lblNomServeur"></asp:Label>
								</td>
								<td rowspan="3">
									<div class="perf-card cpu">
										<div class="perf-card-title">
											CPU
										</div>
										<div class="perf-card-valeur">
											<asp:Label runat="server" ID="lblCPUValeur"></asp:Label>
										</div>
									</div>
								</td>
								<td rowspan="3">
									<div class="perf-card ram">
										<div class="perf-card-title">
											RAM Dispo
										</div>
										<div class="perf-card-valeur">
											<asp:Label runat="server" ID="lblRAMDispoValeur"></asp:Label>
										</div>
									</div>
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
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>

		<asp:UpdatePanel runat="server" ID="upLogs" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="section">
					<div class="section-title">
						Logs
				&nbsp;
					</div>

					<div class="section-content">

						<asp:Repeater runat="server" ID="rptLogs" OnItemDataBound="rptLogs_ItemDataBound">
							<ItemTemplate>

								<asp:Panel runat="server" CssClass='<%# "log "+Eval("myProjet.myTheme.CssClass") %>'>
									<asp:Label runat="server" Text='<%# Eval("myProjet.libelle") %>' CssClass="log-label-project"></asp:Label>
									-
									<asp:Label runat="server" Text='<%# Eval("libelle") %>' CssClass="log-label-subtitle"></asp:Label>

									<div class="log-buttons">

										<asp:HyperLink runat="server" CssClass="log-btn" ToolTip="Voir le fichier de log" ID="btnSeeFile" Target="_blank">
									<i class="fa fa-eye"></i>
										</asp:HyperLink>
										<asp:LinkButton runat="server" CssClass="log-btn" ToolTip="Télécharger le fichier de log" OnClick="btnDownloadFichier_Click" CommandArgument='<%# Eval("id") %>'>
									<i class="fa fa-download"></i>
										</asp:LinkButton>
									</div>
								</asp:Panel>

							</ItemTemplate>
						</asp:Repeater>

					</div>
				</div>
			</ContentTemplate>

		</asp:UpdatePanel>

		<asp:UpdatePanel runat="server" ID="upDatabase" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="section">
					<div class="section-title">
						Bases de données
						&nbsp; 
					</div>
					<div class="section-content">


						<asp:Repeater runat="server" ID="rptDatabase">
							<ItemTemplate>

								<div class="database-container">
									<asp:Panel runat="server" CssClass='<%# "database "+Eval("myProjet.myTheme.CssClass") %>'>
										<div class="database-title">
											<asp:Label runat="server" Text='<%# Eval("myProjet.libelle")+" - "+Eval("libelle") %>'></asp:Label>
										</div>
										<div class="database-buttons">

											<asp:LinkButton runat="server" CssClass="database-btn" ID="btnSaveDatabase" OnClick="btnSaveDatabase_Click" CommandArgument='<%# Eval("id") %>'>
												<i class="fa fa-floppy-o"></i>
											</asp:LinkButton>
											<asp:LinkButton runat="server" CssClass="database-btn" ID="btnDownload" OnClick="btnDownload_Click" CommandArgument='<%# Eval("id") %>'>
												<i class="fa fa-download"></i>
											</asp:LinkButton>
										</div>
									</asp:Panel>
								</div>

							</ItemTemplate>
						</asp:Repeater>

						<div class="clear"></div>
					</div>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>
</asp:Content>
