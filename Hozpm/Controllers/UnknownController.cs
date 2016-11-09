using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hozpm.Controllers
{
	public class UnknownController : Controller
	{
		private const string Message = "The controller for path '{0}' is UnknownController.";

		public ActionResult Index()
		{
			throw new HttpException((int) HttpStatusCode.NotFound, string.Format(Message, Request.Path));
		}
	}
}