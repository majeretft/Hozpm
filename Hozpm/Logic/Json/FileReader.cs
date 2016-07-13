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

		public T GetToken<T>(string folder, string fileName) where T: JToken
		{
			if (string.IsNullOrEmpty(folder))
				throw new ArgumentNullException(nameof(folder));

			var path = Path.Combine(folder, fileName);

			if (!File.Exists(path))
				throw new FileNotFoundException($"File '{fileName}' not found in '{folder}'.");

			using (var file = File.OpenText(path))
			using (var reader = new JsonTextReader(file))
			{
				return (T) JToken.ReadFrom(reader);
			}
		}

		public JArray GetGroups(string folder)
		{
			var fr = new FileReader();
			return fr.GetToken<JArray>(folder, GroupsFile);
		}

		public JArray GetPurposes(string folder)
		{
			var fr = new FileReader();
			return fr.GetToken<JArray>(folder, PurposesFile);
		}

		public JArray GetProducts(string folder)
		{
			var fr = new FileReader();
			return fr.GetToken<JArray>(folder, ProductsFile);
		}

		public JArray GetKits(string folder)
		{
			var fr = new FileReader();
			return fr.GetToken<JArray>(folder, KitsFile);
		}
	}
}