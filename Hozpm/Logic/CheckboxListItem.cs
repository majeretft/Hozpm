using System;
using System.Collections.Generic;

namespace Hozpm.Logic
{
	public class CheckboxListItem : IEquatable<CheckboxListItem>
	{
		public bool Selected { get; set; }
		public string Text { get; set; }
		public string Value { get; set; }

		public bool Equals(CheckboxListItem other)
		{
			if (ReferenceEquals(this, other))
				return true;

			return other != null && Value.Equals(other.Value);
		}
	}

	public class CheckboxListItemComparer : IEqualityComparer<CheckboxListItem>
	{
		public bool Equals(CheckboxListItem x, CheckboxListItem y)
		{
			return x.Equals(y);
		}

		public int GetHashCode(CheckboxListItem obj)
		{
			return obj.Value.GetHashCode();
		}
	}
}