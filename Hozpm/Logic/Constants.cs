using System.Collections.Generic;
using System.Web.Mvc;

namespace Hozpm.Logic
{
	public static class Constants
	{
		public static class Form
		{
			public const DisplayEnum DisplaySelectedDefault = DisplayEnum.Ten;
			public const DisplayEnum DisplaySelectedAll = DisplayEnum.All;
			public const OrderEnum OrderSelectedDefault = OrderEnum.Code;
			public const int GroupSelectedDefault = 0;
			public const int PageNumberDefault = 1;

			public static IEnumerable<SelectListItem> DisplayList => new List<SelectListItem>
			{
				new SelectListItem { Selected = true, Value = DisplayEnum.Ten.ToString(), Text = ((int)DisplayEnum.Ten).ToString()},
				new SelectListItem { Value = DisplayEnum.Twenty.ToString(), Text = ((int)DisplayEnum.Twenty).ToString()},
				new SelectListItem { Value = DisplayEnum.Thirty.ToString(), Text = ((int)DisplayEnum.Thirty).ToString()},
				new SelectListItem { Value = DisplayEnum.All.ToString(), Text = "Все"}
			};

			public static IEnumerable<SelectListItem> OrderList => new List<SelectListItem>
			{
				new SelectListItem { Selected = true, Value = OrderEnum.Code.ToString(), Text = "Артикул" },
				new SelectListItem { Value = OrderEnum.Caption.ToString(), Text = "Наименование" }
			};

			public static IEnumerable<SelectListItem> VolumeList => new List<SelectListItem>
			{
				new SelectListItem { Selected = true, Value = VolumeEnum.MiliLiter.ToString(), Text = "мл"},
				new SelectListItem { Value = VolumeEnum.Liter.ToString(), Text = "л"}
			};

			public static IEnumerable<SelectListItem> WeightList => new List<SelectListItem>
			{
				new SelectListItem { Selected = true, Value = WeightEnum.Gram.ToString(), Text = "г"},
				new SelectListItem { Value = WeightEnum.KiloGram.ToString(), Text = "кг"}
			};
		}

		public static class Og
		{
			public const string HeadPrefixProduct = "product: http://ogp.me/ns/product#";
			public const string HeadPrefixBusiness = "business: http://ogp.me/ns/business#";
		}

		public static class Ids
		{
			/// <summary>
			/// Флюсы
			/// </summary>
			public const string GroupFlux = "0";
			/// <summary>
			/// Флюс-гели
			/// </summary>
			public const string GroupFluxeGel = "1";
			/// <summary>
			/// Флюс-люксы
			/// </summary>
			public const string GroupFluxeLux = "2";
			/// <summary>
			/// Припои и сплавы
			/// </summary>
			public const string GroupAlloyAndSolder = "3";
			/// <summary>
			/// Клеи
			/// </summary>
			public const string GroupGlue = "4";
			/// <summary>
			/// Лаки
			/// </summary>
			public const string GroupLacquer = "5";
			/// <summary>
			/// Наборы
			/// </summary>
			public const string GroupKit = "6";
			/// <summary>
			/// Очистители, пасты, железо хлорное
			/// </summary>
			public const string GroupCleanerPasteIron = "7";
			/// <summary>
			/// Смазочные материалы
			/// </summary>
			public const string GroupLubricant = "8";
			/// <summary>
			/// Разное
			/// </summary>
			public const string GroupOther = "9";
		}

		public const string CompanyName = "Группа компаний «Паяльные материалы»";
		public const string CompanyEmail = "hozpm@mail.ru";
		public static List<string> CompanyPhones = new List<string> { "+7 (4912) 27-17-55", "+7 (4912) 95-06-42", "+7 (4912) 95-06-43" };

		public const string EmptyPhotoPath = "placeholder/no-photo.jpg";
	}
}