﻿using System.Web.Optimization;

namespace Hozpm
{
	public class BundleConfig
	{
		public static void Register(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/css/bootstrap.css")
				.Include("~/Content/bootstrap.css"));
			bundles.Add(new StyleBundle("~/css/bootstrap-theme.css")
				.Include("~/Content/bootstrap-theme.css"));
			bundles.Add(new StyleBundle("~/css/site.css")
				.Include("~/css/site.css"));

			bundles.Add(new StyleBundle("~/css/user.css")
				.Include("~/css/user.css"));

			bundles.Add(new ScriptBundle("~/js/jquery.js")
				.Include("~/Scripts/jquery-{version}.js"));
			bundles.Add(new ScriptBundle("~/js/bootstrap.js")
				.Include("~/Scripts/bootstrap.js"));

			bundles.Add(new ScriptBundle("~/js/knockout.js")
				.Include("~/Scripts/knockout-{version}.js"));
		}
	}
}