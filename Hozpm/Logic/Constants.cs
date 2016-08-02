namespace Hozpm.Logic
{
	public static class Constants
	{
		public static class Form
		{
			public const int DisplaySelectedDefault = 10;
			public const OrderEnum OrderSelectedDefault = OrderEnum.Code;
			public const int GroupSelectedDefault = 0;
			public const int PageNumberDefault = 1;
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

		public const string CompanyName = "Группа компаний AVRGroup";
	}
}