using System;
using System.Collections.Generic;
using System.Linq;

namespace Hozpm.Logic.Entities
{
	public class FormSettings
	{
		public string DisplaySelected { get; set; }
		public string OrderSelected { get; set; }
		public string GroupSelected { get; set; }
		public string Code { get; set; }
		public bool? GroupAny { get; set; }
		public bool? PurposeAny { get; set; }
		public IEnumerable<CheckboxListItem> Purposes { get; set; }

		public int GetDisplaySelected
		{
			get
			{
				int result;
				return int.TryParse(DisplaySelected, out result) ? result : Constants.Form.DisplaySelectedDefault;
			}
		}

		public int GetGroupSelected
		{
			get
			{
				int result;
				return int.TryParse(GroupSelected, out result) ? result : Constants.Form.GroupSelectedDefault;
			}
		}

		public OrderEnum GetOrderSelected
		{
			get
			{
				OrderEnum result;
				return Enum.TryParse(OrderSelected, out result) ? result : Constants.Form.OrderSelectedDefault;
			}
		}

		public string GetCode
		{
			get
			{
				var code = Code?.Trim();
				return string.IsNullOrEmpty(code) ? string.Empty : $"A{code}";
			}
		} 

		public bool GetGroupAny => GroupAny.HasValue && GroupAny.Value;

		public bool GetPurposeAny => PurposeAny.HasValue && PurposeAny.Value;

		public bool HasSelectedPurposes => Purposes.Any(x => x.Selected);

		public IEnumerable<int> GetSelectedPurposes
		{
			get
			{
				var result = new List<int>();

				foreach (var checkboxListItem in Purposes.Where(x => x.Selected))
				{
					int value;
					if (int.TryParse(checkboxListItem.Value, out value))
						result.Add(value);
				}

				return result;
			}
		} 
	}
}