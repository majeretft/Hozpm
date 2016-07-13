﻿using System.Collections.Generic;
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

		public IEnumerable<SelectListItem> Groups { get; set; }
	}
}