<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="ServerMonitoring_fw" %>
<%@ Import Namespace="ServerMonitoring_fw.BIZ" %>

<script RunAt="server">

	void Application_Start(object sender, EventArgs e)
	{
		// Code that runs on application startup

	}

	void Application_End(object sender, EventArgs e)
	{
		//  Code that runs on application shutdown

	}

	void Application_Error(object sender, EventArgs e)
	{
		// Code that runs when an unhandled error occurs
		try
		{
			DirectoryInfo myDir = new DirectoryInfo(Param.Logs);
			if (!myDir.Exists)
				myDir.Create();
			if (myDir.Exists)
			{
				HttpApplication info = (HttpApplication)sender;
				FileInfo myLogFile = new FileInfo(Path.Combine(myDir.FullName, "WebError.txt"));
				using (StreamWriter sw = new StreamWriter(myLogFile.FullName, true))
				{
					sw.WriteLine("****************************************************************");
					sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - Une erreur a été relevée.");
					if (info.Context.Error != null)
					{
						if (info.Context.Error.Message != null)
							sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - Message : " + info.Context.Error.Message);
						if (info.Context.Error.InnerException != null)
							sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - InnerException : " + info.Context.Error.InnerException.ToString());
						if (info.Context.Error.StackTrace != null)
							sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - StackTrace : " + info.Context.Error.StackTrace.ToString());
						if (info.Context.Error.Source != null)
							sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - Source : " + info.Context.Error.Source.ToString());
						if (info.Context.Error.TargetSite != null)
							sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - TargetSite : " + info.Context.Error.TargetSite.ToString());
					}
					sw.WriteLine("");
					sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - INFORMATIONS SUPPLEMENTAIRES :");
					if (info.Context.Handler != null)
						sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - Handler : " + info.Context.Handler.ToString());
					if (Request.Url != null)
						sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - URL : " + Request.Url.ToString());
					if (Request.Browser != null)
						sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - Browers : " + Request.Browser.Browser);
					sw.WriteLine("");

					sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - FORM :");
					if (Request.Form != null)
					{

					}
					sw.WriteLine("");
					sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - SESSION :");
					if (MySession.GetSession("user") != null)
					{
						object user = MySession.GetSession("user");
						if (user != null)
						{

							Utilisateur myUser = (Utilisateur)user;
							sw.WriteLine("Utilisateur Login : " + myUser.login.ToString());

						}


					}

					sw.WriteLine("");

				}
			}
			Server.ClearError();
		}
		catch
		{

		}
	}

	void Session_Start(object sender, EventArgs e)
	{
		// Code that runs when a new session is started

	}

	void Session_End(object sender, EventArgs e)
	{
		// Code that runs when a session ends. 
		// Note: The Session_End event is raised only when the sessionstate mode
		// is set to InProc in the Web.config file. If session mode is set to StateServer 
		// or SQLServer, the event is not raised.

	}
       
</script>
