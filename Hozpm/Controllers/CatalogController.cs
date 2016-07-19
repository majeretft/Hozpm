using System;
using System.Web.Mvc;
using System.Web.UI;
using Hozpm.Logic;
using Hozpm.Logic.Abstract;
using Hozpm.Logic.Entities;
using Hozpm.Logic.Exception;
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
		[OutputCache(Duration = 600, Location = OutputCacheLocation.Any)]
		public ViewResult Index()
		{
			var model = _modelProvider.GetCatalogHomeViewModel();

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

		[OutputCache(Duration = 600, Location = OutputCacheLocation.Any)]
		public ActionResult Product(string item)
		{
			if (string.IsNullOrEmpty(item))
			{
				HandleNotFoundError(new ProductItemNotFoundException($"Product item not found (uri = {item})"));
				return RedirectToAction("Http404", "Error");
			}

			ProductViewModel model;

			if (_modelProvider.TryGetProductViewModel(item, out model))
				return View(model);

			HandleNotFoundError(new ProductItemNotFoundException($"Product item not found (uri = {item})"));
			return RedirectToAction("Http404", "Error");
		}

		[OutputCache(Duration = 600, Location = OutputCacheLocation.Any)]
		public ActionResult Kit(string item)
		{
			if (string.IsNullOrEmpty(item))
			{
				HandleNotFoundError(new ProductItemNotFoundException($"Product kit not found (uri = {item})"));
				return RedirectToAction("Http404", "Error");
			}

			KitViewModel model;

			if (_modelProvider.TryGetKitViewModel(item, out model))
				return View(model);

			HandleNotFoundError(new ProductItemNotFoundException($"Product kit not found (uri = {item})"));
			return RedirectToAction("Http404", "Error");
		}

		public ViewResult ViewAll()
		{
			var model = _modelProvider.GetCatalogHomeViewModel();

			return View(model);
		}

		private static void HandleNotFoundError(Exception ex)
		{
			Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
		}
	}
}