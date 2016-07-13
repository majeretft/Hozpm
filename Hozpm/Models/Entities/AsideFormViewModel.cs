using System.Collections.Generic;
using System.Web.Mvc;

namespace Hozpm.Models.Entities
{
	public class AsideFormViewModel
	{
		public class Group
		{
			public int Id { get; set; }
			public string CaptionUri { get; set; }
			public string Caption { get; set; }
		}

		public bool GroupAny { get; set; }
		public string GroupSelected { get; set; }
		public IEnumerable<SelectListItem> Groups { get; set; }

		public bool PurposeAny { get; set; }
	}
}