using System.Web.Mvc;
using System.Web.Routing;

namespace Hozpm
{
	public class RouteConfig
	{
		public static void Register(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("Error500", "Error/Http500", new { controller = "Error", action = "Http500" });
			routes.MapRoute("Error404", "Error/Http404", new { controller = "Error", action = "Http404" });
			routes.MapRoute("Error", "Error", new { controller = "Error", action = "Index" });
			routes.MapRoute("Item", "Catalog/{action}/{item}", new { controller = "Catalog", action = "Index" });
			routes.MapRoute("Default", "{controller}/{action}", new { controller = "Home", action = "Index" });
		}
	}
}