using System.Collections.Generic;

namespace Hozpm.Logic.Entities
{
	public abstract class ProductBase
	{
		private string _photoPath;

		public int Id { get; set; }
		public int GroupId { get; set; }
		public IEnumerable<int> PurposeIds { get; set; }
		public string Uri { get; set; }
		public string Caption { get; set; }
		public string CaptionShort { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }

		public string PhotoPath
		{
			get
			{
				return _photoPath;
			}
			set
			{
				_photoPath = string.IsNullOrEmpty(value) ? Constants.EmptyPhotoPath : value;
			}
		}

		public Container Container { get; set; }

		public abstract bool GetIsKit { get; }

		public DataSeo Seo { get; set; }
		public DataOg Og { get; set; }
		public DataSchema Schema { get; set; }
	}
}