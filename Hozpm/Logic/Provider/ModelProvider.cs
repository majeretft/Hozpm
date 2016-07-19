﻿using System.Linq;
using System.Web.Mvc;
using Hozpm.Logic.Abstract;
using Hozpm.Logic.Entities;
using Hozpm.Models;
using Hozpm.Models.Entities;

namespace Hozpm.Logic.Provider
{
	public class ModelProvider : IModelProvider
	{
		private readonly IDataProvider _dataProvider;

		public ModelProvider(IDataProvider dataProvider)
		{
			_dataProvider = dataProvider;
		}

		public CatalogHomeViewModel GetCatalogHomeViewModel()
		{
			var asideViewModel = GetAsideFormViewModel();

			var result = new CatalogHomeViewModel
			{
				FormModel = asideViewModel,
				Items = _dataProvider.GetItems()
			};

			return result;
		}

		public CatalogHomeViewModel GetCatalogHomeViewModel(FormSettings formSettings)
		{
			var itemsFluent = _dataProvider.GetFluentItems();

			// Ordering all items before all operations
			itemsFluent.Order(formSettings.GetOrderSelected);

			// Item code has maximum priority because it is uqique and gives only one item
			var code = formSettings.GetCode;
			if (!string.IsNullOrEmpty(code))
			{
				itemsFluent.WithCode(code);
			}
			else
			{
				// if group is set and "any group" checkbox is unchecked
				if (!formSettings.GetGroupAny && formSettings.GetGroupSelected >= 0)
				{
					itemsFluent.WithGroup(formSettings.GetGroupSelected);
				}

				// if there are any selected purposes and "any purpose" checkbox is unchecked
				if (!formSettings.GetPurposeAny && formSettings.HasSelectedPurposes)
				{
					itemsFluent.WithPurposes(formSettings.GetSelectedPurposes);
				}
			}

			// Selecting top X items after all operations
			var displayCount = formSettings.GetDisplaySelected;
			if (displayCount > 0)
				itemsFluent.Take(displayCount);

			// Getting the result items list
			var items = itemsFluent.ToEnumerable();

			var asideViewModel = GetAsideFormViewModel(formSettings);

			var result = new CatalogHomeViewModel
			{
				FormModel = asideViewModel,
				Items = items
			};

			result.FilterCode = result.FormModel.Code;
			result.FilterGroup = result.FormModel.GetSelectedGroupText;
			result.FilterPurposes = result.FormModel.GetSelectedPurposesText;

			return result;
		}

		public bool TryGetProductViewModel(string uri, out ProductViewModel result)
		{
			var product = _dataProvider.GetProduct(uri);

			if (product == null)
			{
				result = null;
				return false;
			}

			var analogicProducts = product.AnalogyId.HasValue 
				? _dataProvider.GetAnalogicProducts(product.Id, product.AnalogyId.Value) 
				: null;
			var includedInKits = _dataProvider.GetRelativeKits(product.Id);

			result = new ProductViewModel
			{
				Product = product,
				AnalogicProducts = analogicProducts,
				RelativeKits = includedInKits
			};

			return true;
		}

		public bool TryGetKitViewModel(string uri, out KitViewModel result)
		{
			var item = _dataProvider.GetKit(uri);

			if (item == null)
			{
				result = null;
				return false;
			}

			var included = _dataProvider.GetIncludedProducts(item.ProductsIncluded);

			result = new KitViewModel
			{
				Kit = item,
				IncludedProducts = included
			};

			return true;
		}

		protected AsideFormViewModel GetAsideFormViewModel()
		{
			var groups = _dataProvider.GetGroups();
			var selectListItems = groups.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Text
			}).ToList();

			if (selectListItems.Any())
				selectListItems.First().Selected = true;

			var purposes = _dataProvider.GetPurposes();
			var checkboxListItems = purposes.Select(x => new CheckboxListItem
			{
				Value = x.Id.ToString(),
				Text = x.Text
			}).ToList();

			var result = new AsideFormViewModel
			{
				Groups = selectListItems,
				Purposes = checkboxListItems
			};

			return result;
		}

		protected AsideFormViewModel GetAsideFormViewModel(FormSettings formSettings)
		{
			var result = GetAsideFormViewModel();

			result.GroupAny = formSettings.GetGroupAny;
			result.PurposeAny = formSettings.GetPurposeAny;
			result.GroupSelected = formSettings.GetGroupSelected.ToString();
			result.Code = formSettings.GetCode;
			result.DisplaySelected = formSettings.GetDisplaySelected.ToString();
			result.OrderSelected = formSettings.GetOrderSelected.ToString();

			if (!formSettings.HasSelectedPurposes)
				return result;

			var purposesToModify = formSettings
				.GetSelectedPurposes
				.Select(selectedPurpose => result.Purposes.FirstOrDefault(x => x.Value == selectedPurpose.ToString()))
				.Where(purpose => purpose != null);

			foreach (var purpose in purposesToModify)
				purpose.Selected = true;

			return result;
		}
	}
}