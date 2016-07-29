using System.Net;
using System.Web.Mvc;

namespace Hozpm.Controllers
{
	public class ErrorController : Controller
	{
		public ActionResult Index(string code)
		{
			HttpContext.Response.Clear();
			HttpContext.Response.TrySkipIisCustomErrors = true;

			int c;
			Response.StatusCode = int.TryParse(code, out c) && c != 0
				? c
				: (int) HttpStatusCode.InternalServerError;

			return View();
		}

		public ActionResult Http404()
		{
			HttpContext.Response.Clear();
			HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
			HttpContext.Response.TrySkipIisCustomErrors = true;

			return View();
		}

		public ActionResult Http500()
		{
			HttpContext.Response.Clear();
			HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
			HttpContext.Response.TrySkipIisCustomErrors = true;

			return View();
		}
	}
}