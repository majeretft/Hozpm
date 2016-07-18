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
			var asideViewModel = GetAsideFormViewModel();

			var result = new CatalogHomeViewModel
			{
				FormModel = asideViewModel,
				Items = _dataProvider.GetItems()
			};

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
				Id = x.Id.ToString(),
				Text = x.Text
			}).ToList();

			var result = new AsideFormViewModel
			{
				Groups = selectListItems,
				Purposes = checkboxListItems
			};

			return result;
		}
	}
}