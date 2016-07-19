using System;
using System.Collections.Generic;
using System.Linq;

namespace Hozpm.Logic.Entities
{
	public class ItemListFluent
	{
		private IEnumerable<ProductBase> _items;

		public int Count => _items.Count();

		public ItemListFluent(IEnumerable<ProductBase> items)
		{
			_items = items;
		}

		public ItemListFluent Order(OrderEnum orderBy)
		{
			switch (orderBy)
			{
				case OrderEnum.Code:
					_items = _items.OrderBy(x => x.Code);
					break;
				case OrderEnum.Caption:
					_items = _items.OrderBy(x => x.Caption);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(orderBy), orderBy, null);
			}

			return this;
		}

		public ItemListFluent Skip(int skip)
		{
			if (skip < 0)
				throw new ArgumentOutOfRangeException(nameof(skip));

			_items = _items.Skip(skip);

			return this;
		}

		public ItemListFluent Take(int take)
		{
			if (take == 0)
				return this;

			if (take < 0)
				throw new ArgumentOutOfRangeException(nameof(take));

			_items = _items.Take(take);

			return this;
		}

		public ItemListFluent WithGroup(int groupId)
		{
			if (groupId < 0)
				throw new ArgumentOutOfRangeException(nameof(groupId));

			_items = _items.Where(x => x.GroupId == groupId);

			return this;
		}

		public ItemListFluent WithPurposes(IEnumerable<int> purposeIds)
		{
			if (purposeIds == null)
				throw new ArgumentNullException(nameof(purposeIds));

			var filter = purposeIds.ToList();

			if (!filter.Any())
			{
				_items = new List<ProductBase>(0);
				return this;
			}

			_items = _items.Where(x => filter.Any(y => x.PurposeIds.Contains(y)));

			return this;
		}

		public ItemListFluent WithCode(string code)
		{
			if (string.IsNullOrEmpty(code))
				throw new ArgumentNullException(nameof(code));

			_items = _items.Where(x => code.Equals(x.Code, StringComparison.InvariantCultureIgnoreCase));

			return this;
		}

		public IEnumerable<ProductBase> ToEnumerable()
		{
			return _items;
		}
	}
}