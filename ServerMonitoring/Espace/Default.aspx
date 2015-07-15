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
					<asp:Timer runat="server" ID="timerPerf" Interval="500" OnTick="timerPerf_Tick"></asp:Timer>
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
				<asp:LinkButton runat="server" CssClass="log-add" ID="btnAddLog" OnClick="btnAddLog_Click">
					<i class="fa fa-plus add"></i>
				</asp:LinkButton>
					</div>

					<div class="section-content">

						<asp:Panel runat="server" ID="pnlEditLog" CssClass="edit-log" Visible="false">
							<div class="edit-log-title">
								<asp:Label runat="server" ID="lblEditLogTitle" Text="Ajouter un log"></asp:Label>
							</div>
							<div class="edit-log-content">
								<table>
									<thead>
										<tr>
											<th>Projet</th>
											<th>Chemin</th>
											<th>
												<asp:Button runat="server" ID="btnEditLogAnnuler" Text="Annuler" CssClass="btn btn-block btn-lg btn-danger" OnClick="btnEditLogAnnuler_Click" /></th>
										</tr>
									</thead>
									<tbody>
										<tr>
											<td>
												<asp:DropDownList runat="server" ID="ddlEditLogProjet" CssClass="form-control">
													<asp:ListItem Value="1" Text="Activa"></asp:ListItem>
													<asp:ListItem Value="2" Text="iFaxNet"></asp:ListItem>
													<asp:ListItem Value="3" Text="iZyFrais"></asp:ListItem>
													<asp:ListItem Value="4" Text="iWi"></asp:ListItem>
												</asp:DropDownList>
											</td>
											<td>
												<asp:TextBox runat="server" ID="tbEditLogChemin" placeholder="Chemin du fichier" CssClass="form-control"></asp:TextBox>
											</td>
											<td>

												<asp:Button runat="server" ID="btnEditLogSave" Text="Sauvegarder" CssClass="btn btn-block btn-lg btn-info" OnClick="btnEditLogSave_Click" />
											</td>
										</tr>
									</tbody>
								</table>
							</div>
						</asp:Panel>


						<asp:Repeater runat="server" ID="rptLogs" OnItemDataBound="rptLogs_ItemDataBound">
							<ItemTemplate>

								<asp:Panel runat="server" CssClass='<%# "log "+Eval("myProjet.myTheme.CssClass") %>'>
									<asp:Label runat="server" Text='<%# Eval("myProjet.libelle") %>' CssClass="log-label-project"></asp:Label>
									-
									<asp:Label runat="server" Text='<%# Eval("libelle") %>' CssClass="log-label-subtitle"></asp:Label>

									<div class="log-buttons">
										<asp:LinkButton runat="server" CssClass="log-btn" ToolTip="Paramètres" OnClick="btnConfigLog_Click" CommandArgument='<%# Eval("id") %>'>
									<i class="fa fa-cogs"></i>
										</asp:LinkButton>
										<asp:HyperLink runat="server" CssClass="log-btn" ToolTip="Voir le fichier de log" ID="btnSeeFile">
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
			</ContentTemplate>

		</asp:UpdatePanel>

		<asp:UpdatePanel runat="server" ID="upDatabase" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="section">
					<div class="section-title">
						Bases de données
						&nbsp;
				<asp:LinkButton runat="server" CssClass="log-add">
					<i class="fa fa-plus add"></i>
				</asp:LinkButton>
					</div>
					<div class="section-content">

						<div class="database-container">
							<div class="database blue">
								<div class="database-title">
									Activa
								</div>
								<div class="database-buttons">
									<asp:LinkButton runat="server" CssClass="database-btn">
									<i class="fa fa-cogs"></i>
									</asp:LinkButton>
									<asp:LinkButton runat="server" CssClass="database-btn">
									<i class="fa fa-floppy-o"></i>
									</asp:LinkButton>
									<asp:LinkButton runat="server" CssClass="database-btn">
									<i class="fa fa-download"></i>
									</asp:LinkButton>
								</div>
							</div>
						</div>

						<div class="database-container">
							<div class="database orange">
								<div class="database-title">
									iZyFrais
								</div>
								<div class="database-buttons">
									<asp:LinkButton runat="server" CssClass="database-btn">
									<i class="fa fa-cogs"></i>
									</asp:LinkButton>
									<asp:LinkButton runat="server" CssClass="database-btn">
									<i class="fa fa-floppy-o"></i>
									</asp:LinkButton>
									<asp:LinkButton runat="server" CssClass="database-btn">
									<i class="fa fa-download"></i>
									</asp:LinkButton>
								</div>
							</div>
						</div>

						<div class="database-container">
							<div class="database red">
								<div class="database-title">
									iFaxNet
								</div>
								<div class="database-buttons">
									<asp:LinkButton runat="server" CssClass="database-btn">
									<i class="fa fa-cogs"></i>
									</asp:LinkButton>
									<asp:LinkButton runat="server" CssClass="database-btn">
									<i class="fa fa-floppy-o"></i>
									</asp:LinkButton>
									<asp:LinkButton runat="server" CssClass="database-btn">
									<i class="fa fa-download"></i>
									</asp:LinkButton>
								</div>
							</div>
						</div>

						<div class="database-container">
							<div class="database green">
								<div class="database-title">
									IDDIC
								</div>
								<div class="database-buttons">
									<asp:LinkButton runat="server" CssClass="database-btn">
									<i class="fa fa-cogs"></i>
									</asp:LinkButton>
									<asp:LinkButton runat="server" CssClass="database-btn">
									<i class="fa fa-floppy-o"></i>
									</asp:LinkButton>
									<asp:LinkButton runat="server" CssClass="database-btn">
									<i class="fa fa-download"></i>
									</asp:LinkButton>
								</div>
							</div>
						</div>

						<div class="database-container">
							<div class="database grey">
								<div class="database-title">
									iWi
								</div>
								<div class="database-buttons">
									<asp:LinkButton runat="server" CssClass="database-btn">
									<i class="fa fa-cogs"></i>
									</asp:LinkButton>
									<asp:LinkButton runat="server" CssClass="database-btn">
									<i class="fa fa-floppy-o"></i>
									</asp:LinkButton>
									<asp:LinkButton runat="server" CssClass="database-btn">
									<i class="fa fa-download"></i>
									</asp:LinkButton>
								</div>
							</div>
						</div>

						<div class="clear"></div>
					</div>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>
</asp:Content>
