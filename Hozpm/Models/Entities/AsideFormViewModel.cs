using System.Collections.Generic;
using System.Linq;
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

		public string DisplaySelected { get; set; }
		public List<SelectListItem> DisplayList => new List<SelectListItem>
		{
			new SelectListItem { Selected = true, Value = "10", Text = "10"},
			new SelectListItem { Value = "20", Text = "20"},
			new SelectListItem { Value = "30", Text = "30"},
			new SelectListItem { Value = "0", Text = "Все"}
		};

		public string OrderSelected { get; set; }
		public List<SelectListItem> OrderList => new List<SelectListItem>
		{
			new SelectListItem { Selected = true, Value = "code", Text = "Артикул"},
			new SelectListItem { Value = "caption", Text = "Наименование"}
		};

		public string GetSelectedGroupText
		{
			get
			{
				return GroupAny ? "Любая группа" : Groups.FirstOrDefault(x => x.Value == GroupSelected)?.Text;
			}
		}

		public List<string> GetSelectedPurposesText
		{
			get
			{
				return PurposeAny ? new List<string> {"Любое назначение"} : Purposes.Where(x => x.Selected).Select(x => x.Text).ToList();
			}
		}
	}
}