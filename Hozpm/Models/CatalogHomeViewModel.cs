using System.Collections.Generic;
using Hozpm.Models.Entities;

namespace Hozpm.Models
{
	public class CatalogHomeViewModel
	{
		public AsideFormViewModel FormModel { get; set; }

		public string FilterCode { get; set; }
		public string FilterGroup { get; set; }
		public List<string> FilterPurposes { get; set; }
	}
}