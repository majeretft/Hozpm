using System.Web.Mvc;
using Hozpm.Logic;
using Hozpm.Models;

namespace Hozpm.Controllers
{
	public class CatalogController : Controller
	{
		public ViewResult Index(string groupSelected, bool? groupAny, bool? purposeAny)
		{
			var mb = new ModelBuilder(Server.MapPath("~/App_Data/json"));
			var formModel = mb.GetAsideFormViewModel();

			if (groupAny.HasValue)
				formModel.GroupAny = groupAny.Value;
			if (purposeAny.HasValue)
				formModel.PurposeAny = purposeAny.Value;
			if (!string.IsNullOrEmpty(groupSelected))
				formModel.GroupSelected = groupSelected;

			var model = new CatalogHomeViewModel { FormModel = formModel };

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