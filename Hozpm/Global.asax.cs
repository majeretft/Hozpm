using System;
using System.Web.Optimization;
using System.Web.Routing;

namespace Hozpm
{
	public class Global : System.Web.HttpApplication
	{

		protected void Application_Start(object sender, EventArgs e)
		{
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
	}
}