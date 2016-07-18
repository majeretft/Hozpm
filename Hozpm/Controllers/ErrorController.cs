using System.Net;
using System.Web.Mvc;

namespace Hozpm.Controllers
{
	public class ErrorController : Controller
	{
		public ActionResult Index(string code)
		{
			int c;
			if (int.TryParse(code, out c))
				Response.StatusCode = c;

			return View();
		}

		public ActionResult Http404()
		{
			Response.StatusCode = (int) HttpStatusCode.NotFound;

			return View();
		}

		public ActionResult Http500()
		{
			Response.StatusCode = (int) HttpStatusCode.InternalServerError;

			return View();
		}
	}
}