using System.Collections.Generic;
using System.Web.Mvc;
using Hozpm.Logic.Entities;
using Hozpm.Models.Entities;

namespace Hozpm.Models
{
	[Bind(Include = "FormModel")]
	public class CatalogHomeViewModel
	{
		public string FilterCode { get; set; }
		public string FilterGroup { get; set; }
		public IEnumerable<string> FilterPurposes { get; set; }

		/// <summary>
		/// Only this property should be bound from request
		/// </summary>
		public AsideFormViewModel FormModel { get; set; }

		public IEnumerable<ProductBase> Items { get; set; }
		public PaginationViewModel PaginationModel { get; set; }
		public bool RequiresPagination { get; set; }

		public CatalogHomeViewModel()
		{
			RequiresPagination = false;
		}
	}
}