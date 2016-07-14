﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hozpm.Logic.Json;
using Hozpm.Models;
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

		public CatalogHomeViewModel GetCatalogHomeViewModel()
		{
			var asideViewModel = GetAsideFormViewModel();
			var result = new CatalogHomeViewModel
			{
				FormModel = asideViewModel,
				Products = GetProducts(_jsonFolderPath),
				Kits = GetKits(_jsonFolderPath)
			};

			return result;
		}

		private AsideFormViewModel GetAsideFormViewModel()
		{
			var result = new AsideFormViewModel
			{
				Groups = GetGroups(_jsonFolderPath),
				Purposes = GetPurposes(_jsonFolderPath),
			};

			return result;
		}

		private List<SelectListItem> GetGroups(string folder)
		{
			var fr = new FileReader();
			var token = fr.GetGroups(folder);

			var p = new DataParser();
			var items = p.ParseFilterRuleList(token);

			var result = items.Select(x => new SelectListItem
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
			var items = p.ParseFilterRuleList(token);

			var result = items.Select(x => new CheckboxListModel
			{
				Text = x.Caption,
				Value = x.CaptionUri
			}).ToList();

			return result;
		}

		private List<CatalogHomeViewModel.Product> GetProducts(string folder)
		{
			var fr = new FileReader();
			var token = fr.GetProducts(folder);

			var p = new DataParser();
			return p.ParseProductList(token);
		}

		private List<CatalogHomeViewModel.Product> GetKits(string folder)
		{
			var fr = new FileReader();
			var token = fr.GetKits(folder);

			var p = new DataParser();
			var items = p.ParseProductList(token);

			foreach (var item in items)
				item.IsKit = true;

			return items;
		}
	}
}