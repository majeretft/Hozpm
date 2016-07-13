using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hozpm.Logic.Json
{
	public class FileReader
	{
		public const string GroupsFile = "groups.json";

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
	}
}