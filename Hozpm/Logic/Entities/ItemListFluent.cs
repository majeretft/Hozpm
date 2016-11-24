using System;
using System.Collections.Generic;
using System.Linq;

namespace Hozpm.Logic.Entities
{
	public class ItemListFluent
	{
		private IEnumerable<ProductBase> _items;

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

		public IEnumerable<ProductBase> ToEnumerable()
		{
			return _items;
		}

		public ItemListFluent WithCode(string code)
		{
			if (string.IsNullOrEmpty(code))
				throw new ArgumentNullException(nameof(code));

			_items = _items.Where(x => x.Code.EndsWith(code));

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

		public ItemListFluent WithVolume(double? from, double? to, VolumeEnum digit)
		{
			var digitItem = Constants.Form.VolumeList.FirstOrDefault(x => x.Value == digit.ToString());

			if (digitItem == null)
				throw new ArgumentOutOfRangeException(nameof(digit), $"Digit {digit} does not have correspinding value in Constants.Form.VolumeList");

			var text = digitItem.Text;

			if (from.HasValue && from.Value > 0)
				_items = _items.Where(x => 
					x.Container?.Volume != null 
					&& x.Container.Volume.Value >= from.Value
					&& text.Equals(x.Container.VolumeText, StringComparison.InvariantCultureIgnoreCase)
				);

			if (to.HasValue && to.Value > 0)
				_items = _items.Where(x => 
					x.Container?.Volume != null 
					&& x.Container.Volume.Value <= to.Value 
					&& text.Equals(x.Container.VolumeText, StringComparison.InvariantCultureIgnoreCase)
				);

			return this;
		}

		public ItemListFluent WithWeight(double? from, double? to, WeightEnum digit)
		{
			var digitItem = Constants.Form.WeightList.FirstOrDefault(x => x.Value == digit.ToString());

			if (digitItem == null)
				throw new ArgumentOutOfRangeException(nameof(digit), $"Digit {digit} does not have correspinding value in Constants.Form.WeightList");

			var text = digitItem.Text;

			if (from.HasValue && from.Value > 0)
				_items = _items.Where(x => 
					x.Container?.Weight != null 
					&& x.Container.Weight.Value >= from.Value 
					&& text.Equals(x.Container.WeightText, StringComparison.InvariantCultureIgnoreCase)
				);

			if (to.HasValue && to.Value > 0)
				_items = _items.Where(x => 
					x.Container?.Weight != null 
					&& x.Container.Weight.Value <= to.Value
					&& text.Equals(x.Container.WeightText, StringComparison.InvariantCultureIgnoreCase)
				);

			return this;
		}
	}
}