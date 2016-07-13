using System.Collections.Generic;
using System.Web.Mvc;
using Hozpm.Logic;

namespace Hozpm.Models.Entities
{
	public class AsideFormViewModel
	{
		public class FilterRule
		{
			public int Id { get; set; }
			public string CaptionUri { get; set; }
			public string Caption { get; set; }
		}

		public bool GroupAny { get; set; }
		public string GroupSelected { get; set; }
		public List<SelectListItem> Groups { get; set; }

		public bool PurposeAny { get; set; }
		public List<CheckboxListModel> Purposes { get; set; }

		public string Code { get; set; }
	}
}