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

		private readonly string _folder;

		public FileReader(string folder)
		{
			_folder = folder;
		}

		public JToken GetToken(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
				throw new ArgumentNullException(nameof(fileName));

			var path = Path.Combine(_folder, fileName);

			if (!File.Exists(path))
				throw new FileNotFoundException($"File '{fileName}' not found in '{_folder}'.");

			using (var file = File.OpenText(path))
			using (var reader = new JsonTextReader(file))
			{
				return JToken.ReadFrom(reader);
			}
		}

		public JArray GetGroups()
		{
			var items = GetToken(GroupsFile);

			return items as JArray;
		}

		public JArray GetPurposes()
		{
			var items = GetToken(PurposesFile);

			return items as JArray;
		}

		public JArray GetProducts()
		{
			var items = GetToken(ProductsFile);

			return items as JArray;
		}

		public JArray GetKits()
		{
			var items = GetToken(KitsFile);

			return items as JArray;
		}
	}
}