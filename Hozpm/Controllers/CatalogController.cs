using System.Web.Mvc;
using Hozpm.Logic;
using Hozpm.Models;

namespace Hozpm.Controllers
{
	public class CatalogController : Controller
	{
		public ActionResult Index()
		{
			var mb = new ModelBuilder(Server.MapPath("~/App_Data/json"));
			var formModel = mb.GetAsideFormViewModel();

			var model = new CatalogHomeViewModel {FormModel = formModel};

			return View(model);
		}

		public ActionResult Product()
		{
			return View();
		}

		public ActionResult Kit()
		{
			return View();
		}
	}
}