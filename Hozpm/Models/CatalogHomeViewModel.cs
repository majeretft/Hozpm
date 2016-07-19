using System.Collections.Generic;
using Hozpm.Logic.Entities;
using Hozpm.Models.Entities;

namespace Hozpm.Models
{
	public class CatalogHomeViewModel
	{
		public AsideFormViewModel FormModel { get; set; }

		public IEnumerable<ProductBase> Items { get; set; }

		public string FilterCode { get; set; }
		public string FilterGroup { get; set; }
		public IEnumerable<string> FilterPurposes { get; set; }

		public bool RequiresPagination { get; set; }
		public PaginationViewModel PaginationModel { get; set; }
	}
}