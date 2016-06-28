using System.Web.Mvc;
using System.Web.Routing;

namespace Hozpm
{
	public class RouteConfig
	{
		public static void Register(RouteCollection routes)
		{
			routes.MapRoute("Default", "{controller}/{action}", new { controller = "Home", action = "Index" });
		}
	}
}