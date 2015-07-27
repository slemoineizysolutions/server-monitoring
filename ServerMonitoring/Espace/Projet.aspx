<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Projet.aspx.cs" Inherits="Espace_Projet" MasterPageFile="~/MP/MP.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="iZyWebServerControl" Namespace="iZyWebServerControl" TagPrefix="iZy" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
	<link rel="stylesheet" type="text/css" href="../CSS/projet.css" />
	<link rel="stylesheet" type="text/css" href="../CSS/dashboard.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
	<div class="content">
		<asp:UpdatePanel runat="server" ID="upGeneral" UpdateMode="Conditional">
			<ContentTemplate>

				<asp:Panel runat="server" ID="pnlPageTitle" CssClass="page-title">
					<asp:HyperLink runat="server" CssClass="back" ID="hlBackProjet" NavigateUrl="~/Espace/Projets.aspx">
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
							<asp:Panel runat="server" DefaultButton="btnEditProjet" class="section-content">
								<table class="table-infos">
									<tbody>
										<tr>
											<td>Site de production<br />
												<asp:Panel runat="server" ID="pnlUrlProd" CssClass="textbox-icon">
													<asp:TextBox runat="server" ID="tbUrlProd" CssClass="form-control" placeholder="URL de production"></asp:TextBox>
													<asp:LinkButton runat="server" ID="btnUrlProd" OnClick="btnUrlProd_Click">
														<i class="fa fa-paper-plane"></i>
													</asp:LinkButton>
												</asp:Panel>
											</td>
										</tr>
										<tr>
											<td>Site de test<br />
												<asp:Panel runat="server" ID="pnlUrlTest" CssClass="textbox-icon">
													<asp:TextBox runat="server" ID="tbUrlTest" CssClass="form-control" placeholder="URL de test"></asp:TextBox>
													<asp:LinkButton runat="server" ID="btnUrlTest" OnClick="btnUrlTest_Click">
														<i class="fa fa-paper-plane"></i>
													</asp:LinkButton>
												</asp:Panel>
											</td>
										</tr>
										<tr>
											<td>Thème<br />
												<iZy:iZySwitch runat="server" ID="switchTheme" />
											</td>
										</tr>
										<tr>
											<td>
												<asp:Button runat="server" ID="btnEditProjet" Text="Sauvegarder" CssClass="btn btn-block btn-lg btn-info" OnClick="btnEditProjet_Click" />
												<div style="text-align: center">
													<asp:Label runat="server" ID="lblMessageSauvegarde" CssClass="projet-save-message"></asp:Label>
												</div>
											</td>
										</tr>
								</table>


							</asp:Panel>
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>

				<asp:UpdatePanel runat="server" ID="upLogs" UpdateMode="Conditional">
					<ContentTemplate>
						<div class="section">
							<div class="section-title">
								Fichiers
								&nbsp;
								<asp:LinkButton runat="server" CssClass="log-add" ID="btnAddLog" OnClick="btnAddLog_Click">
									<i class="fa fa-plus add"></i>
								</asp:LinkButton>
							</div>

							<div class="section-content">

								<asp:Panel runat="server" ID="pnlEditLog" CssClass="edit-log" Visible="false">
									<div class="edit-log-title">
										<asp:Label runat="server" ID="lblEditLogTitle" Text="Ajouter un fichier"></asp:Label>
									</div>
									<div class="edit-log-content">
										<table>
											<tbody>
												<tr>
													<td colspan="2">Libellé
														<asp:TextBox runat="server" ID="tbEditLogLibelle" placeholder="Libellé du fichier" CssClass="form-control"></asp:TextBox>
													</td>
												</tr>
												<tr>
													<td>Chemin
														<asp:TextBox runat="server" ID="tbEditLogChemin" placeholder="Chemin du fichier" CssClass="form-control"></asp:TextBox>
													</td>
													<td>
														<asp:Button runat="server" ID="btnEditLogAnnuler" Text="Annuler" CssClass="btn btn-block btn-lg btn-danger" OnClick="btnEditLogAnnuler_Click" />
													</td>
												</tr>
												<tr>
													<td>Commentaire
														<asp:TextBox runat="server" ID="tbEditLogCommentaire" placeholder="Commentaire" CssClass="form-control" Rows="3" TextMode="MultiLine" style="resize: none"></asp:TextBox>
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
												<asp:LinkButton runat="server" ID="btnFavorisLog" CssClass="log-btn" ToolTip="Favoris" OnClick="btnFavorisLog_Click" CommandArgument='<%# Eval("id") %>' CommandName="">
													<i runat="server" id="logFavorisOn" visible="false" class="fa fa-star"></i>
													<i runat="server" id="logFavorisOff" visible="false" class="fa fa-star-o"></i>
												</asp:LinkButton>
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
												<asp:TextBox runat="server" ID="tbEditDatabaseLibelle" placeholder="Libellé" CssClass="form-control"></asp:TextBox>
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


								<asp:Repeater runat="server" ID="rptDatabase" OnItemDataBound="rptDatabase_ItemDataBound">
									<ItemTemplate>

										<div class="database-container">
											<asp:Panel runat="server" CssClass='<%# "database "+Eval("myProjet.myTheme.CssClass") %>'>
												<div class="database-title">
													<asp:Label runat="server" Text='<%# Eval("libelle") %>'></asp:Label>
												</div>
												<div class="database-buttons">
													<asp:LinkButton runat="server" ID="btnFavorisBaseDonnee" CssClass="database-btn fourth" ToolTip="Favoris" OnClick="btnFavorisBaseDonnee_Click" CommandArgument='<%# Eval("id") %>' CommandName="">
														<i runat="server" id="dbFavorisOn" visible="false" class="fa fa-star"></i>
														<i runat="server" id="dbFavorisOff" visible="false" class="fa fa-star-o"></i>
													</asp:LinkButton>
													<asp:LinkButton runat="server" CssClass="database-btn fourth" ID="btnConfigDatabase" OnClick="btnConfigDatabase_Click" CommandArgument='<%# Eval("id") %>'>
														<i class="fa fa-cogs"></i>
													</asp:LinkButton>
													<asp:LinkButton runat="server" CssClass="database-btn fourth" ID="btnSaveDatabase" OnClick="btnSaveDatabase_Click" CommandArgument='<%# Eval("id") %>'>
														<i class="fa fa-floppy-o"></i>
													</asp:LinkButton>
													<asp:LinkButton runat="server" CssClass="database-btn fourth" ID="btnDownload" OnClick="btnDownload_Click" CommandArgument='<%# Eval("id") %>'>
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
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>
</asp:Content>
