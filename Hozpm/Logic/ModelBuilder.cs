using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hozpm.Logic.Entities;
using Hozpm.Logic.Json;
using Hozpm.Models;
using Hozpm.Models.Entities;
using Newtonsoft.Json.Linq;
using Filter = Hozpm.Logic.Entities.Filter;

namespace Hozpm.Logic
{
	public class ModelBuilder
	{
		private readonly string _jsonFolderPath;

		private delegate JToken GetToken(string jsonFolderPath);

		public ModelBuilder(string jsonFolderPath)
		{
			_jsonFolderPath = jsonFolderPath;
		}

		public CatalogHomeViewModel GetCatalogHomeViewModel()
		{
			var asideViewModel = GetAsideFormViewModel();
			var result = new CatalogHomeViewModel
			{
				FormModel = asideViewModel,
				Products = GetProducts(),
				Kits = GetKits()
			};

			return result;
		}

		public ProductViewModel GetProductViewModel(string uri)
		{
			var product = GetProduct(uri);
			var analogicProducts = GetAnalogicProducts(product.Id, product.AnalogyId);
			var includedInKits = GetRelativeKits(product.Id);

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
			var item = GetKit(uri);
			var included = GetIncludedProducts(item.ProductsIncluded);

			var result = new KitViewModel
			{
				Kit = item,
				IncludedProducts = included
			};

			return result;
		}

		private AsideFormViewModel GetAsideFormViewModel()
		{
			var groups = GetFilters(path => new FileReader().GetGroups(path));
			var selectListItems = groups.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Text
			}).ToList();

			if (selectListItems.Any())
				selectListItems.First().Selected = true;

			var purposes = GetFilters(path => new FileReader().GetPurposes(path));
			var checkboxListItems = purposes.Select(x => new CheckboxListModel
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

		private IEnumerable<Filter> GetFilters(GetToken function)
		{
			if (function == null)
				throw new ArgumentNullException(nameof(function));

			var token = function(_jsonFolderPath);

			var p = new DataParser();
			return p.ParseFilters(token);
		}

		private Product GetProduct(string uri)
		{
			var items = GetProducts();

			return items.First(x => x.Uri.Equals(uri));
		}

		private IEnumerable<Product> GetProducts()
		{
			var fr = new FileReader();
			var token = fr.GetProducts(_jsonFolderPath);

			var p = new DataParser();
			return p.ParseProducts(token);
		}

		private IEnumerable<Product> GetAnalogicProducts(int id, int analogyId)
		{
			var products = GetProducts();

			if (products == null)
				return null;

			var p = products.ToList();

			return !p.Any() ? null : p.Where(x => x.Id != id && x.AnalogyId == analogyId);
		}

		private IEnumerable<ProductBase> GetRelativeKits(int productId)
		{
			var kits = GetKits();

			return kits.Where(x => x.ProductsIncluded != null && x.ProductsIncluded.Contains(productId));
		}

		private IEnumerable<ProductBase> GetIncludedProducts(IEnumerable<int> includedProductIds)
		{
			var products = GetProducts();

			return products.Where(x => includedProductIds.Contains(x.Id));
		}

		private IEnumerable<Kit> GetKits()
		{
			var fr = new FileReader();
			var token = fr.GetKits(_jsonFolderPath);

			var p = new DataParser();
			return p.ParseKits(token);
		}

		private Kit GetKit(string uri)
		{
			var items = GetKits();

			return items.First(x => x.Uri.Equals(uri));
		}
	}
}