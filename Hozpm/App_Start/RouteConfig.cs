using System.Web.Mvc;
using System.Web.Routing;

namespace Hozpm
{
	public class RouteConfig
	{
		public static void Register(RouteCollection routes)
		{
			routes.MapRoute("Item", "Catalog/{action}/{item}", new { controller = "Catalog", action = "Index" });
			routes.MapRoute("Default", "{controller}/{action}", new { controller = "Home", action = "Index" });
		}
	}
}