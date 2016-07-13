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
				Groups = GetGroups(_jsonFolderPath),
				Purposes = GetPurposes(_jsonFolderPath)
			};

			return result;
		}

		private List<SelectListItem> GetGroups(string folder)
		{
			var fr = new FileReader();
			var token = fr.GetGroups(folder);

			var p = new DataParser();
			var groups = p.ParseFilterRuleList(token);

			var result = groups.Select(x => new SelectListItem
			{
				Text = x.Caption,
				Value = x.CaptionUri
			}).ToList();

			return result;
		}

		private List<CheckboxListModel> GetPurposes(string folder)
		{
			var fr = new FileReader();
			var token = fr.GetPurposes(folder);

			var p = new DataParser();
			var purposes = p.ParseFilterRuleList(token);

			var result = purposes.Select(x => new CheckboxListModel
			{
				Text = x.Caption,
				Value = x.CaptionUri
			}).ToList();

			return result;
		}
	}
}