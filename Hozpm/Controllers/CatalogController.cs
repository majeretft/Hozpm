using System.Web.Mvc;
using System.Web.UI;
using Hozpm.Logic;
using Hozpm.Logic.Abstract;
using Hozpm.Logic.Entities;
using Hozpm.Models;

namespace Hozpm.Controllers
{
	public class CatalogController : Controller
	{
		private readonly IModelProvider _modelProvider;

		public CatalogController(IModelProvider modelProvider)
		{
			_modelProvider = modelProvider;
		}

		[HttpGet]
		[OutputCache(Duration = 600, Location = OutputCacheLocation.ServerAndClient)]
		public ViewResult Index(string groupSelected)
		{
			var formSettings = new FormSettings
			{
				GroupSelected = groupSelected,
				GroupAny = string.IsNullOrEmpty(groupSelected),
				PurposeAny = true
			};

			var model = _modelProvider.GetCatalogHomeViewModel(formSettings);

			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ViewResult Index(
			string displaySelected, 
			string pageNumber, 
			string orderSelected, 
			string groupSelected, 
			string code, 
			bool? groupAny, 
			bool? purposeAny, 
			params CheckboxListItem[] purposes)
		{
			var formSettings = new FormSettings
			{
				OrderSelected = orderSelected,
				DisplaySelected = displaySelected,
				GroupSelected = groupSelected,
				Code = code,
				GroupAny = groupAny,
				PurposeAny = purposeAny,
				Purposes = purposes,
				PageNumber = pageNumber
			};

			var model = _modelProvider.GetCatalogHomeViewModel(formSettings);
			
			return View(model);
		}

		[OutputCache(Duration = 600, Location = OutputCacheLocation.ServerAndClient)]
		public ActionResult Product(string item)
		{
			if (string.IsNullOrEmpty(item))
				return RedirectToAction("Http404", "Error");

			ProductViewModel model;

			if (_modelProvider.TryGetProductViewModel(item, out model))
				return View(model);

			return RedirectToAction("Http404", "Error");
		}

		[OutputCache(Duration = 600, Location = OutputCacheLocation.ServerAndClient)]
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
			var model = _modelProvider.GetCatalogHomeViewModel(new FormSettings
			{
				GroupAny = true,
				PurposeAny = true,
				DisplaySelected = "0"
			});

			return View(model);
		}
	}
}