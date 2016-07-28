using System;
using System.Collections.Generic;
using System.Linq;
using Hozpm.Logic.Entities;
using Newtonsoft.Json.Linq;

namespace Hozpm.Logic.Json
{
	public class DataParser
	{
		private DataSeo ParseSeo(JToken token)
		{
			if (token == null)
				throw new ArgumentNullException(nameof(token));

			return new DataSeo
			{
				Description = token.Value<string>("description"),
				Keywords = token.Value<string>("keywords")
			};
		}

		private DataOg ParseOg(JToken token)
		{
			if (token == null)
				throw new ArgumentNullException(nameof(token));

			return new DataOg
			{
				Description = token.Value<string>("description"),
				Title = token.Value<string>("title"),
				ImageHeight = token.Value<int?>("imageHeight"),
				ImageType = token.Value<string>("imageType"),
				ImageWidth = token.Value<int?>("imageWidth")
			};
		}

		private DataSchema ParseSchema(JToken token)
		{
			if (token == null)
				throw new ArgumentNullException(nameof(token));

			return new DataSchema
			{
				Description = token.Value<string>("description"),
				Name = token.Value<string>("name")
			};
		}

		private Container ParseContainer(JToken token)
		{
			if (token == null)
				throw new ArgumentNullException(nameof(token));

			return new Container
			{
				Weight = token.Value<double?>("weight"),
				Volume = token.Value<double?>("volume"),
				Length = token.Value<double?>("lenght"),
				Width = token.Value<double?>("width"),
				Height = token.Value<double?>("height"),
				WeightText = token.Value<string>("weightText"),
				VolumeText = token.Value<string>("volumeText"),
				LengthText = token.Value<string>("lenghtText"),
				WidthText = token.Value<string>("widthText"),
				HeightText = token.Value<string>("heightText")
			};
		}

		public void ParseProductBase(ProductBase target, JObject token)
		{
			if (target == null)
				throw new ArgumentNullException(nameof(target));
			if (token == null)
				throw new ArgumentNullException(nameof(token));

			target.Caption = token.Value<string>("caption");
			target.CaptionShort = token.Value<string>("captionShort");
			target.Code = token.Value<string>("code");
			target.Description = token.Value<string>("description");
			target.GroupId = token.Value<int>("groupId");
			target.Id = token.Value<int>("id");
			target.PhotoPath = token.Value<string>("photoPath");
			target.Uri = token.Value<string>("uri");

			var purposes = token["purposeIds"];
			if (purposes != null)
				target.PurposeIds = purposes.Values<int>();

			var container = token["container"];
			if (container != null)
				target.Container = ParseContainer(container);

			var seo = token["seo"];
			if (seo != null)
				target.Seo = ParseSeo(seo);

			var og = token["og"];
			if (og != null)
				target.Og = ParseOg(og);

			var schema = token["schema"];
			if (schema != null)
				target.Schema = ParseSchema(schema);
		}

		public IEnumerable<Product> ParseProducts(JToken token)
		{
			if (token == null)
				throw new ArgumentNullException(nameof(token));

			return token
				.OfType<JObject>()
				.Select(x =>
				{
					var result = new Product();
					ParseProductBase(result, x);
					result.AnalogyId = x.Value<int?>("analogyId");
					return result;
				});
		}

		public IEnumerable<Kit> ParseKits(JToken token)
		{
			if (token == null)
				throw new ArgumentNullException(nameof(token));

			return token
				.OfType<JObject>()
				.Select(x =>
				{
					var result = new Kit();
					ParseProductBase(result, x);

					var purposes = x["productsIncluded"];
					if (purposes != null)
						result.ProductsIncluded = purposes.Values<int>();

					return result;
				});
		}

		public IEnumerable<Filter> ParseFilters(JToken token)
		{
			if (token == null)
				throw new ArgumentNullException(nameof(token));

			return token
				.OfType<JObject>()
				.Select(x => new Filter
				{
					Text = x.Value<string>("text"),
					Id = x.Value<int>("id")
				});
		}
	}
}