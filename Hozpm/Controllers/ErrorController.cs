using System.Net;
using System.Web.Mvc;

namespace Hozpm.Controllers
{
	public class ErrorController : Controller
	{
		public ActionResult Index(string code)
		{
			int c;
			Response.StatusCode = int.TryParse(code, out c) && c != 0 
				? c 
				: (int) HttpStatusCode.InternalServerError;

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