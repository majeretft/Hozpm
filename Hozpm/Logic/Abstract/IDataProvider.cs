using System.Collections.Generic;
using Hozpm.Logic.Entities;

namespace Hozpm.Logic.Abstract
{
	public interface IDataProvider
	{
		IEnumerable<Filter> GetGroups();
		IEnumerable<Filter> GetPurposes();

		Product GetProduct(string uri);
		Kit GetKit(string uri);

		IEnumerable<ProductBase> GetItems();
		IEnumerable<Product> GetProducts();
		IEnumerable<Kit> GetKits();
		ItemListFluent GetFluentItems();

		IEnumerable<Product> GetAnalogicProducts(int productId, int analogyId);
		IEnumerable<ProductBase> GetRelativeKits(int productId);
		IEnumerable<ProductBase> GetIncludedProducts(IEnumerable<int> includedProductIds);
	}
}
