using System;
using System.Collections.Generic;
using System.Linq;
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
			return GetCatalogHomeViewModel(new AsideFormViewModel
			{
				GroupAny = true,
				PurposeAny = true
			});
		}

		public CatalogHomeViewModel GetCatalogHomeViewModel(AsideFormViewModel formSettings)
		{
			var result = new CatalogHomeViewModel();

			formSettings.Groups = AssignGroups(formSettings.GroupSelected.ToString()).ToList();
			formSettings.Purposes = AssignPurposes(formSettings.Purposes).ToList();
			result.FormModel = formSettings;

			var items = FilterItems(formSettings).ToList();
			var paginationViewModel = ApplyPaging(formSettings.DisplaySelected, formSettings.PageNumber, ref items);

			result.Items = items;

			result.RequiresPagination = paginationViewModel != null && paginationViewModel.PageCount > 1;
			result.PaginationModel = paginationViewModel;

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

		private PaginationViewModel ApplyPaging(DisplayEnum display, int page, ref List<ProductBase> items)
		{
			var pageSize = (int) display;
			var itemsExceedPage = pageSize > 0 && items.Count > pageSize;

			if (!itemsExceedPage)
				return null;

			var currentPage = page;
			var pageCount = (int) Math.Ceiling((double) items.Count/pageSize);

			var skipCount = (currentPage - 1)*pageSize;
			if (skipCount > 0)
				items = items.Skip(skipCount).ToList();
			items = items.Take(pageSize).ToList();

			var paginationViewModel = new PaginationViewModel
			{
				CurrentPage = currentPage,
				PageCount = pageCount
			};

			return paginationViewModel;
		}

		private IEnumerable<ProductBase> FilterItems(AsideFormViewModel formSettings)
		{
			if (formSettings == null)
				throw new ArgumentNullException(nameof(formSettings));

			var itemsFluent = _dataProvider.GetFluentItems();

			// Ordering all items before all operations
			itemsFluent.Order(formSettings.OrderSelected);

			// Item code has maximum priority because it is uqique and gives only one item
			var code = formSettings.Code?.Trim().TrimStart('A', 'А');
			if (!string.IsNullOrEmpty(code))
				itemsFluent.WithCode(code);

			// if group is set and "any group" checkbox is unchecked
			if (!formSettings.GroupAny)
				itemsFluent.WithGroup(formSettings.GroupSelected);

			// if there are any selected purposes and "any purpose" checkbox is unchecked
			if (!formSettings.PurposeAny && formSettings.Purposes != null)
			{
				var selectedPurposes = formSettings.Purposes
					.Where(x => x.Selected)
					.Select(checkboxListItem => int.Parse(checkboxListItem.Value))
					.ToList();

				itemsFluent.WithPurposes(selectedPurposes);
			}

			itemsFluent
				.WithWeight(formSettings.WeightFrom, formSettings.WeightTo, formSettings.WeightSelected)
				.WithVolume(formSettings.VolumeFrom, formSettings.VolumeTo, formSettings.VolumeSelected);

			// Getting the filtered items list
			return itemsFluent.ToEnumerable().ToList();
		}

		private IEnumerable<CheckboxListItem> AssignPurposes(IEnumerable<CheckboxListItem> existingPurposes)
		{
			var purposes = _dataProvider.GetPurposes()
				.Select(x => new CheckboxListItem
				{
					Text = x.Text,
					Value = x.Id.ToString()
				})
				.ToList();

			if (existingPurposes == null)
				return purposes;

			var existing = existingPurposes.ToList();

			// Set purposes to be selected if any
			if (!existing.Any(x => x.Selected))
				return purposes;

			var intersected = purposes
				.Intersect(existing, new CheckboxListItemComparer())
				.ToList();

			foreach (var checkboxListItem in purposes)
			{
				if (intersected.Contains(checkboxListItem))
					checkboxListItem.Selected = true;
			}

			return purposes;
		}

		private IEnumerable<SelectListItem> AssignGroups(string groupSelected)
		{
			var groups = _dataProvider.GetGroups()
				.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Text
				})
				.ToList();

			if (!string.IsNullOrEmpty(groupSelected) && groups.Any(x => x.Value.Equals(groupSelected)))
			{
				groups
					.First(x => x.Value.Equals(groupSelected))
					.Selected = true;
			}

			return groups;
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