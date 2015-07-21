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
					<asp:Timer runat="server" ID="timerPerf" Interval="1000" OnTick="timerPerf_Tick"></asp:Timer>
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
									<tbody>
										<tr>
											<td>Libellé
												<asp:TextBox runat="server" ID="tbEditLogLibelle" placeholder="Libellé du log" CssClass="form-control"></asp:TextBox>
											</td>
											<td>Projet
												<asp:DropDownList runat="server" ID="ddlEditLogProjet" CssClass="form-control">
												</asp:DropDownList>
											</td>
											<td>

												<asp:Button runat="server" ID="btnEditLogAnnuler" Text="Annuler" CssClass="btn btn-block btn-lg btn-danger" OnClick="btnEditLogAnnuler_Click" />
											</td>
										</tr>
										<tr>
											<td colspan="2">Chemin
												<asp:TextBox runat="server" ID="tbEditLogChemin" placeholder="Chemin du fichier" CssClass="form-control"></asp:TextBox>
											</td>
											<td>
												<asp:HiddenField runat="server" ID="hfLogId" Visible="false" />
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
				<asp:LinkButton runat="server" CssClass="log-add" ID="btnAddDatabase" OnClick="btnAddDatabase_Click">
					<i class="fa fa-plus add"></i>
				</asp:LinkButton>
					</div>
					<div class="section-content">

						<asp:Panel runat="server" ID="pnlEditDatabase" CssClass="edit-db" Visible="false">
							<div class="edit-db-title">
								<asp:Label runat="server" ID="lblEditDatabaseTitle" Text="Ajouter un log"></asp:Label>
							</div>
							<div class="edit-db-content">
								<table>
									<tbody>
										<tr>
											<td colspan="3">Projet
												<asp:DropDownList runat="server" ID="ddlEditDatabaseProjet" CssClass="form-control">
												</asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td>Hôte
												<asp:TextBox runat="server" ID="tbEditDatabaseHost" placeholder="Hôte" CssClass="form-control"></asp:TextBox>
											</td>
											<td colspan="2">Base de donnée
												<asp:TextBox runat="server" ID="tbEditDatabaseName" placeholder="Nom de la base de données" CssClass="form-control"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td>Utilisateur
												<asp:TextBox runat="server" ID="tbEditDatabaseUser" placeholder="Num d'utilisateur" CssClass="form-control"></asp:TextBox>
											</td>
											<td>Mot de passe
												<asp:TextBox runat="server" ID="tbEditDatabasePassword" placeholder="Mot de passe utilisateur" CssClass="form-control"></asp:TextBox>
											</td>
											<td>
												<asp:Button runat="server" ID="btnEditDatabaseAnnuler" Text="Annuler" CssClass="btn btn-block btn-lg btn-danger" OnClick="btnEditDatabaseAnnuler_Click" />
											</td>
										</tr>

										<tr>
											<td colspan="2">Répertoire de sauvegarde
												<asp:TextBox runat="server" ID="tbEditDatabaseChemin" placeholder="Chemin du répertoire de sauvegarde" CssClass="form-control"></asp:TextBox>
											</td>
											<td>
												<asp:HiddenField runat="server" ID="hfDatabseId" Visible="false" />
												<asp:Button runat="server" ID="btnEditDatabaseSave" Text="Sauvegarder" CssClass="btn btn-block btn-lg btn-info" OnClick="btnEditDatabaseSave_Click" />
											</td>
										</tr>
									</tbody>
								</table>
							</div>
						</asp:Panel>


						<asp:Repeater runat="server" ID="rptDatabase">
							<ItemTemplate>

								<div class="database-container">
									<asp:Panel runat="server" CssClass='<%# "database "+Eval("myProjet.myTheme.CssClass") %>'>
										<div class="database-title">
											<asp:Label runat="server" Text='<%# Eval("myProjet.libelle") %>'></asp:Label>
										</div>
										<div class="database-buttons">
											<asp:LinkButton runat="server" CssClass="database-btn" ID="btnConfigDatabase" OnClick="btnConfigDatabase_Click" CommandArgument='<%# Eval("id") %>'>
												<i class="fa fa-cogs"></i>
											</asp:LinkButton>
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
