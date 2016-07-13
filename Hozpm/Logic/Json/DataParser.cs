using System;
using System.Collections.Generic;
using System.Linq;
using Hozpm.Models;
using Hozpm.Models.Entities;
using Newtonsoft.Json.Linq;

namespace Hozpm.Logic.Json
{
	public class DataParser
	{
		public List<AsideFormViewModel.FilterRule> ParseFilterRuleList(JArray token)
		{
			if (token == null)
				throw new ArgumentNullException(nameof(token));

			return token.OfType<JObject>().Select(ParseFilterRule).ToList();
		}

		public AsideFormViewModel.FilterRule ParseFilterRule(JObject token)
		{
			if (token == null)
				throw new ArgumentNullException(nameof(token));

			var result = new AsideFormViewModel.FilterRule
			{
				Caption = token.Value<string>("caption"),
				CaptionUri = token.Value<string>("captionUri"),
				Id = token.Value<int>("id")
			};

			return result;
		}

		public List<CatalogHomeViewModel.Product> ParseProductList(JArray token)
		{
			if (token == null)
				throw new ArgumentNullException(nameof(token));

			return token.OfType<JObject>().Select(ParseProduct).ToList();
		}

		public CatalogHomeViewModel.Product ParseProduct(JObject token)
		{
			if (token == null)
				throw new ArgumentNullException(nameof(token));

			var result = new CatalogHomeViewModel.Product
			{
				Code = token.Value<string>("code"),
				Caption = token.Value<string>("caption"),
				CaptionUri = token.Value<string>("captionUri"),
				PhotoPath = token.Value<string>("photoPath")
			};

			return result;
		}
	}
}