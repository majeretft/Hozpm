using System.Web.Mvc;
using System.Web.Routing;

namespace Hozpm
{
	public class RouteConfig
	{
		public static void Register(RouteCollection routes)
		{
			routes.LowercaseUrls = true;
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{statics}", new { statics = @".*\.(css|js|gif|jpg|png|xml)(/.)?" });

			routes.MapRoute("Error500", "Error/Http500", new { controller = "Error", action = "Http500" });
			routes.MapRoute("Error404", "Error/Http404", new { controller = "Error", action = "Http404" });
			routes.MapRoute("Error", "Error", new { controller = "Error", action = "Index" });
			routes.MapRoute("Item", "Catalog/{action}/{item}", new { controller = "Catalog", action = "Index" });
			routes.MapRoute("Default", "{controller}/{action}", new { controller = "Home", action = "Index" });
			routes.MapRoute("Unknown", "{*anything}", new { controller = "Unknown", action = "Index" });
		}
	}
}