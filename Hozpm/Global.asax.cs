using System;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using Elmah;
using Hozpm.Logic.Exception;

namespace Hozpm
{
	public class Global : System.Web.HttpApplication
	{

		protected void Application_Start(object sender, EventArgs e)
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
			RouteConfig.Register(RouteTable.Routes);
			BundleConfig.Register(BundleTable.Bundles);
		}

		protected void Session_Start(object sender, EventArgs e)
		{
			// nothing
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			// nothing
		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{
			// nothing
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			// nothing
		}

		protected void Session_End(object sender, EventArgs e)
		{
			// nothing
		}

		protected void Application_End(object sender, EventArgs e)
		{
			// nothing
		}

		protected void ErrorLog_Logged(object sender, ErrorLoggedEventArgs args)
		{
			if (args.Entry.Error.Exception is ProductItemNotFoundException)
				return;

			switch (args.Entry.Error.StatusCode)
			{
				case 404:
					Response.RedirectToRoute("Error404");
					break;
				case 500:
					Response.RedirectToRoute("Error500");
					break;
				default:
					Response.RedirectToRoute("Error", new { code = args.Entry.Error.StatusCode });
					break;
			}

			Response.Clear();
			Server.ClearError();
		}
	}
}