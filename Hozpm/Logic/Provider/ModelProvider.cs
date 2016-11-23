using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hozpm.Logic.Abstract;
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
			return GetCatalogHomeViewModel(new AsideFormViewModel
			{
				GroupAny = true,
				PurposeAny = true
			});
		}

		public CatalogHomeViewModel GetCatalogHomeViewModel(AsideFormViewModel formSettings)
		{
			var itemsFluent = _dataProvider.GetFluentItems();

			formSettings.Groups = _dataProvider.GetGroups()
				.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Text
				})
				.ToList();

			if (formSettings.Groups.Any(x => x.Value.Equals(formSettings.GroupSelected.ToString())))
			{
				formSettings.Groups
					.First(x => x.Value.Equals(formSettings.GroupSelected.ToString()))
					.Selected = true;
			}

			var purposes = _dataProvider.GetPurposes()
				.Select(x => new CheckboxListItem
				{
					Text = x.Text,
					Value = x.Id.ToString()
				})
				.ToList();

			// Set purposes to be selected if any
			if (formSettings.Purposes != null && formSettings.Purposes.Any(x => x.Selected))
			{
				var intersected = purposes
					.Intersect(formSettings.Purposes, new CheckboxListItemComparer())
					.ToList();

				foreach (var checkboxListItem in purposes)
				{
					if (intersected.Contains(checkboxListItem))
						checkboxListItem.Selected = true;
				}
			}
			formSettings.Purposes = purposes;

			// Ordering all items before all operations
			itemsFluent.Order(formSettings.OrderSelected);

			// Item code has maximum priority because it is uqique and gives only one item
			var code = formSettings.Code?.Trim().TrimStart('A', 'А');
			if (!string.IsNullOrEmpty(code))
			{
				itemsFluent.WithCode(code);
			}

			// if group is set and "any group" checkbox is unchecked
			if (!formSettings.GroupAny)
			{
				itemsFluent.WithGroup(formSettings.GroupSelected);
			}

			// if there are any selected purposes and "any purpose" checkbox is unchecked
			if (!formSettings.PurposeAny && formSettings.Purposes != null)
			{
				var selectedPurposes = formSettings.Purposes
					.Where(x => x.Selected)
					.Select(checkboxListItem => int.Parse(checkboxListItem.Value))
					.ToList();

				itemsFluent.WithPurposes(selectedPurposes);
			}

			// Getting the filtered items list
			var items = itemsFluent.ToEnumerable().ToList();

			var pageSize = (int)formSettings.DisplaySelected;
			var itemsExceedPage = pageSize > 0 && items.Count > pageSize;

			PaginationViewModel paginationViewModel = null;
			if (itemsExceedPage)
			{
				var currentPage = formSettings.PageNumber;
				var pageCount = (int) Math.Ceiling((double) items.Count/pageSize);

				var skipCount = (currentPage - 1) * pageSize;
				if (skipCount > 0)
					items = items.Skip(skipCount).ToList();
				items = items.Take(pageSize).ToList();

				paginationViewModel = new PaginationViewModel
				{
					CurrentPage = currentPage,
					PageCount = pageCount
				};
			}

			var result = new CatalogHomeViewModel
			{
				FormModel = formSettings,
				Items = items,
				RequiresPagination = itemsExceedPage,
				PaginationModel = paginationViewModel
			};

			result.FilterCode = result.FormModel.Code;

			result.FilterGroup = 
				formSettings.GroupAny 
					? "любая группа" 
					: formSettings.Groups.Any(x => x.Selected) 
						? formSettings.Groups.First(x => x.Selected).Text
						: "не выбрано ни одной группы";

			result.FilterPurposes =
				formSettings.PurposeAny 
				? new List<string> { "любое назначение" }
				: formSettings.Purposes.Any(x => x.Selected)
					? formSettings.Purposes.Where(x => x.Selected).Select(x => x.Text)
					: new List<string> { "не выбрано ни одного назначения" };

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
	}
}