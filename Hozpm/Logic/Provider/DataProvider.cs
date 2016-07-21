using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Hozpm.Logic.Abstract;
using Hozpm.Logic.Entities;
using Hozpm.Logic.Json;

namespace Hozpm.Logic.Provider
{
	public class DataProvider : IDataProvider
	{
		private readonly FileReader _fr;
		private readonly Cache _cache;

		public DataProvider(string folder)
		{
			_fr = new FileReader(folder);
			_cache = HttpRuntime.Cache;
		}

		public IEnumerable<Filter> GetGroups()
		{
			const string cacheKey = "Groups";

#if !DEBUG
			var cachedResult = _cache[cacheKey] as IEnumerable<Filter>;

			if (cachedResult != null)
				return cachedResult;
#endif

			var token = _fr.GetGroups();
			var result = new DataParser().ParseFilters(token);

			_cache.Add(cacheKey, result, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

			return result;
		}

		public IEnumerable<Filter> GetPurposes()
		{
			const string cacheKey = "Purposes";

#if !DEBUG
			var cachedResult = _cache[cacheKey] as IEnumerable<Filter>;

			if (cachedResult != null)
				return cachedResult;
#endif

			var token = _fr.GetPurposes();
			var result = new DataParser().ParseFilters(token);

			_cache.Add(cacheKey, result, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

			return result;
		}

		public Product GetProduct(string uri)
		{
			if (string.IsNullOrEmpty(uri))
				throw new ArgumentNullException(nameof(uri));

			var items = GetProducts();

			return items.FirstOrDefault(x => x.Uri.Equals(uri));
		}

		public Kit GetKit(string uri)
		{
			if (string.IsNullOrEmpty(uri))
				throw new ArgumentNullException(nameof(uri));

			var items = GetKits();

			return items.FirstOrDefault(x => x.Uri.Equals(uri));
		}

		public IEnumerable<ProductBase> GetItems()
		{
			const string cacheKey = "Items";

#if !DEBUG
			var cachedResult = _cache[cacheKey] as IEnumerable<ProductBase>;

			if (cachedResult != null)
				return cachedResult;
#endif

			var p = GetProducts();
			var k = GetKits();

			var result = new List<ProductBase>();
			result.AddRange(p);
			result.AddRange(k);

			_cache.Add(cacheKey, result, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

			return result;
		}

		public ItemListFluent GetFluentItems()
		{
			return new ItemListFluent(GetItems());
		}

		public IEnumerable<Product> GetAnalogicProducts(int productId, int analogyId)
		{
			if (productId < 0)
				throw new ArgumentOutOfRangeException(nameof(productId));
			if (analogyId < 0)
				throw new ArgumentOutOfRangeException(nameof(analogyId));

			var products = GetProducts();

			return products.Where(x => x.Id != productId && x.AnalogyId == analogyId);
		}

		public IEnumerable<ProductBase> GetRelativeKits(int productId)
		{
			if (productId < 0)
				throw new ArgumentOutOfRangeException(nameof(productId));

			var kits = GetKits();

			return kits.Where(x => x.ProductsIncluded != null && x.ProductsIncluded.Contains(productId));
		}

		public IEnumerable<ProductBase> GetIncludedProducts(IEnumerable<int> includedProductIds)
		{
			if (includedProductIds == null)
				throw new ArgumentNullException(nameof(includedProductIds));

			var list = includedProductIds.ToList();

			if (!list.Any())
				return null;

			var products = GetProducts();

			return products.Where(x => list.Contains(x.Id));
		}

		public IEnumerable<Product> GetProducts()
		{
			const string cacheKey = "Products";

#if !DEBUG
			var cachedResult = _cache[cacheKey] as IEnumerable<Product>;

			if (cachedResult != null)
				return cachedResult;
#endif

			var token = _fr.GetProducts();
			var result = new DataParser().ParseProducts(token);

			_cache.Add(cacheKey, result, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

			return result;
		}

		public IEnumerable<Kit> GetKits()
		{
			const string cacheKey = "Kits";

#if !DEBUG
			var cachedResult = _cache[cacheKey] as IEnumerable<Kit>;

			if (cachedResult != null)
				return cachedResult;
#endif

			var token = _fr.GetKits();
			var result = new DataParser().ParseKits(token);

			_cache.Add(cacheKey, result, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

			return result;
		}
	}
}