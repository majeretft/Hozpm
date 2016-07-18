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

		public ProductViewModel GetProductViewModel(string uri)
		{
			var product = _dataProvider.GetProduct(uri);
			var analogicProducts = _dataProvider.GetAnalogicProducts(product.Id, product.AnalogyId);
			var includedInKits = _dataProvider.GetRelativeKits(product.Id);

			var result = new ProductViewModel
			{
				Product = product,
				AnalogicProducts = analogicProducts,
				RelativeKits = includedInKits
			};

			return result;
		}

		public KitViewModel GetKitViewModel(string uri)
		{
			var item = _dataProvider.GetKit(uri);
			var included = _dataProvider.GetIncludedProducts(item.ProductsIncluded);

			var result = new KitViewModel
			{
				Kit = item,
				IncludedProducts = included
			};

			return result;
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