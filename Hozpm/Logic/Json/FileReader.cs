using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hozpm.Logic.Json
{
	public class FileReader
	{
		public const string GroupsFile = "groups.json";
		public const string PurposesFile = "purposes.json";
		public const string ProductsFile = "products.json";
		public const string KitsFile = "kits.json";

		public JToken GetToken(string folder, string fileName)
		{
			if (string.IsNullOrEmpty(folder))
				throw new ArgumentNullException(nameof(folder));

			var path = Path.Combine(folder, fileName);

			if (!File.Exists(path))
				throw new FileNotFoundException($"File '{fileName}' not found in '{folder}'.");

			using (var file = File.OpenText(path))
			using (var reader = new JsonTextReader(file))
			{
				return JToken.ReadFrom(reader);
			}
		}

		public JArray GetGroups(string folder)
		{
			var fr = new FileReader();
			var items = fr.GetToken(folder, GroupsFile);

			return items as JArray;
		}

		public JArray GetPurposes(string folder)
		{
			var fr = new FileReader();
			var items = fr.GetToken(folder, PurposesFile);

			return items as JArray;
		}

		public JArray GetProducts(string folder)
		{
			var fr = new FileReader();
			var items = fr.GetToken(folder, ProductsFile);

			return items as JArray;
		}

		public JArray GetKits(string folder)
		{
			var fr = new FileReader();
			var items = fr.GetToken(folder, KitsFile);

			return items as JArray;
		}
	}
}