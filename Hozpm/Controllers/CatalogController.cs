using System.Linq;
using System.Web.Mvc;
using Hozpm.Logic;

namespace Hozpm.Controllers
{
	public class CatalogController : Controller
	{
		[HttpGet]
		public ViewResult Index()
		{
			var mb = new ModelBuilder(Server.MapPath("~/App_Data/json"));
			var model = mb.GetCatalogHomeViewModel();

			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ViewResult Index(string displaySelected, string orderSelected, string groupSelected, string code, bool? groupAny, bool? purposeAny, params CheckboxListModel[] purposes)
		{
			var mb = new ModelBuilder(Server.MapPath("~/App_Data/json"));
			var model = mb.GetCatalogHomeViewModel();
			var formModel = model.FormModel;

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

			model.FormModel = formModel;
			model.FilterCode = formModel.Code;
			model.FilterGroup = formModel.GetSelectedGroupText;
			model.FilterPurposes = formModel.GetSelectedPurposesText;

			return View(model);
		}

		public ActionResult Product(string item)
		{
			return View();
		}

		public ActionResult Kit()
		{
			return View();
		}
	}
}