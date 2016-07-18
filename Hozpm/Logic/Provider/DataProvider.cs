using System.Collections.Generic;
using Hozpm.Logic.Abstract;
using Hozpm.Logic.Entities;

namespace Hozpm.Logic.Provider
{
	public class DataProvider : IDataProvider
	{
		public IEnumerable<Filter> GetGroups()
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<Filter> GetPurposes()
		{
			throw new System.NotImplementedException();
		}

		public Product GetProduct(string uri)
		{
			throw new System.NotImplementedException();
		}

		public Kit GetKit(string uri)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<ProductBase> GetItems()
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<Product> GetAnalogicProducts(int productId, int analogyId)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<ProductBase> GetRelativeKits(int productId)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<ProductBase> GetIncludedProducts(IEnumerable<int> includedProductIds)
		{
			throw new System.NotImplementedException();
		}
	}
}