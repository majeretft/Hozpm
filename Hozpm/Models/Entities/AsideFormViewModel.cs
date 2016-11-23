using System.Collections.Generic;
using System.Web.Mvc;
using Hozpm.Logic;

namespace Hozpm.Models.Entities
{
	[Bind(Exclude = "Groups")]
	public class AsideFormViewModel
	{
		/// <summary>
		/// Page number for external pagination control
		/// </summary>
		public int PageNumber { get; set; }
		
		public string Code { get; set; }

		public bool GroupAny { get; set; }
		public int GroupSelected { get; set; }
		/// <summary>
		/// Not required to be bound
		/// </summary>
		public IEnumerable<SelectListItem> Groups { get; set; }

		public bool PurposeAny { get; set; }
		public IEnumerable<CheckboxListItem> Purposes { get; set; }

		public DisplayEnum DisplaySelected { get; set; }
		public OrderEnum OrderSelected { get; set; }

		public double? WeightFrom { get; set; }
		public double? WeightTo { get; set; }
		public WeightEnum WeightSelected { get; set; }

		public double? VolumeFrom { get; set; }
		public double? VolumeTo { get; set; }
		public VolumeEnum VolumeSelected { get; set; }

		public AsideFormViewModel()
		{
			PurposeAny = true;
			PageNumber = 1;
			Code = string.Empty;
			GroupAny = true;
			DisplaySelected = DisplayEnum.Ten;
			OrderSelected = OrderEnum.Code;
			WeightSelected = WeightEnum.Gram;
			VolumeSelected = VolumeEnum.MiliLiter;
		}
	}
}