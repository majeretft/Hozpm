using System.Web.Mvc;

namespace Hozpm.Controllers
{
	public class UserController : Controller
	{
		[HttpGet, AllowAnonymous]
		public ViewResult Login()
		{
			return View();
		}
	}
}