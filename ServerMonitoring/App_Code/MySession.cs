using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using iZyTools.Crypto;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for MySession
/// </summary>
public static class MySession
{
	public static string GetApplicationUrl()
	{
		HttpContext ctx = HttpContext.Current;
		string toReturn = (string)ctx.Cache.Get("ApplicationUrl");

		if (String.IsNullOrEmpty(toReturn))
		{
			HttpRequest hr = HttpContext.Current.Request;
			string host = hr.Url.Host;
			string port = hr.Url.Port.ToString();
			string protocol = "http://";
			if (hr.IsSecureConnection) protocol = "https://";
			if (port.Equals("80") || port.Equals("443"))
				port = String.Empty;
			else
				port = ":" + port;

			toReturn = protocol + host + port;
			if (hr.ApplicationPath != "/")
				toReturn += hr.ApplicationPath;

			ctx.Cache.Insert("ApplicationUrl", toReturn);
		}
		return toReturn;
	}

	public static string SESSIONID
	{
		get
		{
			string getParam = GetParam("SESSIONID");
			if (getParam != null)
			{
				return getParam;
			}
			Page page = (Page)HttpContext.Current.Handler;
			if (page != null && page.Master != null)
			{
				HiddenField myHiddenField = (HiddenField)page.Master.FindControl("SESSIONID");
				if (myHiddenField != null && !String.IsNullOrEmpty(myHiddenField.Value))
				{
					return myHiddenField.Value;
				}
			}
			return null;
		}
	}

	public const string key = "45FTE5896GUIJKK452632145EDZSFT4G"; //Sur 32 caractères
	public const string IV = "4585FGB1G583GHE7";//Sur 16 caractères

	public static object GetSession(string value)
	{
		object result = null;
		if (SESSIONID != string.Empty)
		{
			if (HttpContext.Current.Session[SESSIONID + value] != null)
			{
				result = HttpContext.Current.Session[SESSIONID + value];
			}
		}
		return result;
	}

	public static void SetSession(string name, object value)
	{
		if (SESSIONID != string.Empty)
		{
			HttpContext.Current.Session[SESSIONID + name] = value;
		}
	}

	public static string GenerateGetParams()
	{
		if (SESSIONID != null)
		{
			return "?IS=" + iZyCrypt.Encrypte("SESSIONID=" + SESSIONID, key, IV);
		}
		return string.Empty;
	}

	public static string GenerateGetParams(string param)
	{
		if (SESSIONID != null)
			return "?IS=" + iZyCrypt.Encrypte(param + "&SESSIONID=" + SESSIONID, key, IV);
		return "?IS=" + iZyCrypt.Encrypte(param, key, IV);
	}

	public static string GenerateGetParamsWithNewGUID(string newGUID)
	{
		if (newGUID != null)
		{
			return "?IS=" + iZyCrypt.Encrypte("SESSIONID=" + newGUID, key, IV);
		}
		return string.Empty;
	}

	public static string GenerateGetParamsWithNewGUID(string param, string newGUID)
	{
		if (newGUID != null)
			return "?IS=" + iZyCrypt.Encrypte(param + "&SESSIONID=" + newGUID, key, IV);
		return "?IS=" + iZyCrypt.Encrypte(param, key, IV);
	}

	public static string GetParam(string param)
	{
		string result = null;
		if (HttpContext.Current.Request.Params != null && HttpContext.Current.Request.Params["IS"] != null)
		{
			string parameters = HttpContext.Current.Request.Params["IS"];
			try
			{
				string decryptParameters = iZyCrypt.Decrypte(parameters, key, IV);
				string[] paramTable = decryptParameters.Split('&');
				if (paramTable != null && paramTable.Count() > 0)
				{
					foreach (string item in paramTable)
					{
						if (item.StartsWith(param + "="))
						{
							result = item.Substring(param.Length + 1, item.Length - param.Length - 1);
						}
					}
				}
			}
			catch (System.Security.Cryptography.CryptographicException)
			{
				HttpContext.Current.Response.Redirect("~/Default.aspx");
			}

		}
		return result;
	}

	public static string GetParams()
	{
		string result = null;
		if (HttpContext.Current.Request.Params != null && HttpContext.Current.Request.Params["IS"] != null)
		{
			string parameters = HttpContext.Current.Request.Params["IS"];
			try
			{
				string decryptParameters = iZyCrypt.Decrypte(parameters, key, IV);
				string[] paramTable = decryptParameters.Split('&');
				if (paramTable != null && paramTable.Count() > 0)
				{
					foreach (string item in paramTable)
					{
						if (item.Contains("=") && !item.Contains("SESSIONID="))
						{
							result += item + "&";
						}
					}
				}
			}
			catch (System.Security.Cryptography.CryptographicException)
			{
					HttpContext.Current.Response.Redirect("~/Default.aspx" + MySession.GenerateGetParams());
			}
		}
		if (result != null && result.Length > 0)
			result = result.Substring(0, result.Length - 1);
		return result;
	}

	public static void LogOut()
	{
		if (SESSIONID != null)
		{
			List<string> SessionToKick = new List<string>();
			foreach (string key in HttpContext.Current.Session.Keys)
			{
				if (key != null && key.StartsWith(SESSIONID))
				{
					SessionToKick.Add(key);
				}
			}
			foreach (string item in SessionToKick)
			{
				HttpContext.Current.Session.Remove(item);
			}
		}
	}

	public static void DuplicateSession(string oldGUID, string guid)
	{
		if (oldGUID != null && oldGUID.Length > 0)
		{
			List<string> oldKeys = new List<string>();
			foreach (string key in HttpContext.Current.Session.Keys)
			{
				if (key != null && key.StartsWith(oldGUID))
				{
					oldKeys.Add(key);
				}
			}

			foreach (string key in oldKeys)
			{
				HttpContext.Current.Session.Add(key.Replace(oldGUID, guid), HttpContext.Current.Session[key]);
			}
		}
	}

	
}