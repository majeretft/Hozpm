using System;
using System.Collections.Generic;
using System.Linq;
#if !DEBUG
using System.Web;
using System.Web.Caching;
#endif
using Hozpm.Logic.Abstract;
using Hozpm.Logic.Entities;
using Hozpm.Logic.Json;

namespace Hozpm.Logic.Provider
{
	public class DataProvider : IDataProvider
	{
		private readonly FileReader _fr;
#if !DEBUG
		private readonly Cache _cache;
#endif
		private delegate object ReadData();
		private static readonly object Lock = new object();

		public DataProvider(string folder)
		{
			_fr = new FileReader(folder);
#if !DEBUG
			_cache = HttpRuntime.Cache;
#endif
		}

		public IEnumerable<Filter> GetGroups()
		{
			const string cacheKey = "Groups";

			var result = GetCachedValue<Filter>(cacheKey, () =>
			{
				var token = _fr.GetGroups();
				return new DataParser().ParseFilters(token);
			});

			return result;
		}

		public IEnumerable<Filter> GetPurposes()
		{
			const string cacheKey = "Purposes";

			var result = GetCachedValue<Filter>(cacheKey, () =>
			{
				var token = _fr.GetPurposes();
				return new DataParser().ParseFilters(token);
			});

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

			var p = GetProducts();
			var k = GetKits();

			var result = GetCachedValue<ProductBase>(cacheKey, () =>
			{
				var items = new List<ProductBase>();
				items.AddRange(p);
				items.AddRange(k);

				return items;
			});

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

			var result = GetCachedValue<Product>(cacheKey, () =>
			{
				var token = _fr.GetProducts();
				return new DataParser().ParseProducts(token);
			});

			return result;
		}

		public IEnumerable<Kit> GetKits()
		{
			const string cacheKey = "Kits";

			var result = GetCachedValue<Kit>(cacheKey, () =>
			{
				var token = _fr.GetKits();
				return new DataParser().ParseKits(token);
			});

			return result;
		}

		private IEnumerable<T> GetCachedValue<T>(string cacheKey, ReadData readData) where T : class
		{
			if (string.IsNullOrEmpty(cacheKey))
				throw new ArgumentNullException(nameof(cacheKey));
			if (readData == null)
				throw new ArgumentNullException(nameof(readData));

#if !DEBUG
			var cachedResult = _cache[cacheKey] as IEnumerable<T>;
			if (cachedResult != null)
				return cachedResult;
#endif

			lock (Lock)
			{
#if !DEBUG
				cachedResult = _cache[cacheKey] as IEnumerable<T>;
				if (cachedResult != null)
					return cachedResult;
#endif
				var result = readData();
#if !DEBUG
				_cache.Add(cacheKey, result, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
#endif
				return result as IEnumerable<T>;
			}
		}
	}
}