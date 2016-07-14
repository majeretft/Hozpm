using System.Collections.Generic;

namespace Hozpm.Logic.Entities
{
	public abstract class ProductBase
	{
		public int Id { get; set; }
		public int GroupId { get; set; }
		public IEnumerable<int> PurposeIds { get; set; }
		public string Uri { get; set; }
		public string Caption { get; set; }
		public string CaptionShort { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		public string PhotoPath { get; set; }
		public Container Container { get; set; }

		public abstract bool GetIsKit { get; }
	}
}