using System;
using System.Net;
using System.Web.Optimization;
using System.Web.Routing;
using Hozpm.Logic;

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
			var exc = Server.GetLastError();

			try
			{
				ExceptionUtility.LogException(exc);
			}
			catch(Exception error)
			{
				Response.Clear();
				Response.StatusCode = (int) HttpStatusCode.InternalServerError;

				Response.Write("<h1>Error handler exception</h1>");
				Response.Write("<hr/>");

				Response.Write("<h2>Logger exception details</h2>");
				Response.Write($"<p>Type: {error.GetType()}</p>");
				Response.Write($"<p>Message: {error.Message}</p>");
				Response.Write($"<p>Source: {error.Source}</p>");
				Response.Write("<p>Stack trace:</p>");
				if (error.StackTrace != null)
					Response.Write($"<pre>{error.StackTrace}</pre>");

				Response.Write("<hr/>");

				Response.Write("<h2>Original exception</h2>");
				Response.Write($"<p>Type: {exc.GetType()}</p>");
				Response.Write($"<p>Message: {exc.Message}</p>");
				Response.Write($"<p>Source: {exc.Source}</p>");
				Response.Write("<p>Stack trace:</p>");
				if (error.StackTrace != null)
					Response.Write($"<pre>{exc.StackTrace}</pre>");

				if (exc.InnerException != null)
				{
					Response.Write("<hr/>");

					Response.Write("<h2>Original inner exception</h2>");
					Response.Write($"<p>Type: {exc.InnerException.GetType()}</p>");
					Response.Write($"<p>Message: {exc.InnerException.Message}</p>");
					Response.Write($"<p>Source: {exc.InnerException.Source}</p>");
					Response.Write("<p>Stack trace:</p>");
					if (error.StackTrace != null)
						Response.Write($"<pre>{exc.InnerException.StackTrace}</pre>");
				}

				Server.ClearError();
			}
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