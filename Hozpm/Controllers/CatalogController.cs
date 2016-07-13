using System.Linq;
using System.Web.Mvc;
using Hozpm.Logic;
using Hozpm.Models;

namespace Hozpm.Controllers
{
	public class CatalogController : Controller
	{
		[HttpGet]
		public ViewResult Index()
		{
			var mb = new ModelBuilder(Server.MapPath("~/App_Data/json"));
			var formModel = mb.GetAsideFormViewModel();

			var model = new CatalogHomeViewModel { FormModel = formModel };

			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ViewResult Index(string displaySelected, string orderSelected, string groupSelected, string code, bool? groupAny, bool? purposeAny, params CheckboxListModel[] purposes)
		{
			var mb = new ModelBuilder(Server.MapPath("~/App_Data/json"));
			var formModel = mb.GetAsideFormViewModel();

			if (groupAny.HasValue)
				formModel.GroupAny = groupAny.Value;
			if (purposeAny.HasValue)
				formModel.PurposeAny = purposeAny.Value;
			if (!string.IsNullOrEmpty(groupSelected))
				formModel.GroupSelected = groupSelected;
			if (!string.IsNullOrEmpty(code))
				formModel.Code = code;
			if (!string.IsNullOrEmpty(displaySelected))
				formModel.DisplaySelected = displaySelected;
			if (!string.IsNullOrEmpty(orderSelected))
				formModel.OrderSelected = orderSelected;
			if (purposes != null && purposes.Any(x => x.Selected))
			{
				foreach (var p in purposes)
				{
					var purpose = formModel.Purposes.FirstOrDefault(x => x.Value == p.Value);
					if (purpose == null)
						continue;

					purpose.Selected = p.Selected;
				}
			}

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