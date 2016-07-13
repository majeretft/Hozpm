using System;
using System.Collections.Generic;
using System.Linq;
using Hozpm.Models.Entities;
using Newtonsoft.Json.Linq;

namespace Hozpm.Logic.Json
{
	public class GroupParser
	{
		public IEnumerable<AsideFormViewModel.Group> ParseList(JArray token)
		{
			if (token == null)
				throw new ArgumentNullException(nameof(token));

			return token.OfType<JObject>().Select(Parse).ToList();
		}

		public AsideFormViewModel.Group Parse(JObject token)
		{
			if (token == null)
				throw new ArgumentNullException(nameof(token));

			var result = new AsideFormViewModel.Group
			{
				Caption = token.Value<string>("caption"),
				CaptionUri = token.Value<string>("captionUri"),
				Id = token.Value<int>("id")
			};

			return result;
		}
	}
}