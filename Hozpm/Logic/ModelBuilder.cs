using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hozpm.Logic.Json;
using Hozpm.Models.Entities;

namespace Hozpm.Logic
{
	public class ModelBuilder
	{
		private readonly string _jsonFolderPath;

		public ModelBuilder(string jsonFolderPath)
		{
			_jsonFolderPath = jsonFolderPath;
		}

		public AsideFormViewModel GetAsideFormViewModel()
		{
			var result = new AsideFormViewModel
			{
				Groups = GetGroups(_jsonFolderPath)
			};

			return result;
		}

		private IEnumerable<SelectListItem> GetGroups(string folder)
		{
			var fr = new FileReader();
			var token = fr.GetGroups(folder);

			var p = new GroupParser();
			var groups = p.ParseList(token);

			var result = groups.Select(x => new SelectListItem
			{
				Text = x.Caption,
				Value = x.CaptionUri
			});

			return result;
		}
	}
}