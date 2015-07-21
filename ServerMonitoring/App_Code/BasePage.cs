using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ServerMonitoring_fw;

/// <summary>
/// Description résumée de BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
	protected override object LoadPageStateFromPersistenceMedium()
	{
		return MySession.GetSession("_ViewState");
	}
	protected override void SavePageStateToPersistenceMedium(object viewState)
	{
		MySession.SetSession("_ViewState", viewState);
	}

	protected Utilisateur GetUtilisateur()
	{
		return (Utilisateur)MySession.GetSession("user");
	}
}