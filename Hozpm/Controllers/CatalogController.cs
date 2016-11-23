using System.Web.Mvc;
using System.Web.UI;
using Hozpm.Logic;
using Hozpm.Logic.Abstract;
using Hozpm.Models;
using Hozpm.Models.Entities;

namespace Hozpm.Controllers
{
	[OutputCache(Duration = 300, Location = OutputCacheLocation.ServerAndClient)]
	public class CatalogController : Controller
	{
		private readonly IModelProvider _modelProvider;

		public CatalogController(IModelProvider modelProvider)
		{
			_modelProvider = modelProvider;
		}

		[HttpGet]
		[OutputCache(NoStore = true, Duration = 0, Location = OutputCacheLocation.None)]
		public ViewResult Index(int? groupSelected)
		{
			var formSettings = new AsideFormViewModel
			{
				GroupSelected = groupSelected ?? 0,
				GroupAny = !groupSelected.HasValue,
				PurposeAny = true
			};

			var model = _modelProvider.GetCatalogHomeViewModel(formSettings);

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[OutputCache(NoStore = true, Duration = 0, Location = OutputCacheLocation.None)]
		public ViewResult Index(AsideFormViewModel settings)
		{
			var model = _modelProvider.GetCatalogHomeViewModel(settings);
			
			return View(model);
		}

		public ActionResult Product(string item)
		{
			if (string.IsNullOrEmpty(item))
				return RedirectToAction("Http404", "Error");

			ProductViewModel model;

			if (_modelProvider.TryGetProductViewModel(item, out model))
				return View(model);

			return RedirectToAction("Http404", "Error");
		}

		public ActionResult Kit(string item)
		{
			if (string.IsNullOrEmpty(item))
				return RedirectToAction("Http404", "Error");

			KitViewModel model;

			if (_modelProvider.TryGetKitViewModel(item, out model))
				return View(model);

			return RedirectToAction("Http404", "Error");
		}

		public ViewResult ViewAll()
		{
			var model = _modelProvider.GetCatalogHomeViewModel(new AsideFormViewModel
			{
				GroupAny = true,
				PurposeAny = true,
				DisplaySelected = Constants.Form.DisplaySelectedAll
			});

			return View(model);
		}
	}
}