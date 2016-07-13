using System.Collections.Generic;
using Hozpm.Models.Entities;

namespace Hozpm.Models
{
	public class CatalogHomeViewModel
	{
		public class Product
		{
			public string Code { get; set; }
			public string Caption { get; set; }
			public string CaptionUri { get; set; }
			public string PhotoPath { get; set; }
		}

		public AsideFormViewModel FormModel { get; set; }

		public List<Product> Products { get; set; }

		public string FilterCode { get; set; }
		public string FilterGroup { get; set; }
		public List<string> FilterPurposes { get; set; }
	}
}