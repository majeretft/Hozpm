using System.Web.Mvc;
using System.Web.UI;

namespace Hozpm.Controllers
{
	[OutputCache(Duration = 600, Location = OutputCacheLocation.ServerAndClient)]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult CustomerInfo()
		{
			return View();
		}

		public ActionResult PackingInfo()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Contact()
		{
			return View();
		}
	}
}