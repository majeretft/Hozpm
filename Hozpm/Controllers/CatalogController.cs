using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using Hozpm.Logic;
using Hozpm.Logic.Abstract;
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
		public ViewResult Index(string displaySelected, string orderSelected, string groupSelected, string code, bool? groupAny, bool? purposeAny, params CheckboxListItem[] purposes)
		{
			var model = _modelProvider.GetCatalogHomeViewModel();
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

		private static void HandleNotFoundError(Exception ex)
		{
			Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
		}
	}
}