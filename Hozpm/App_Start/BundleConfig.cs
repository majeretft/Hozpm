using System.Web.Optimization;

namespace Hozpm
{
	public class BundleConfig
	{
		public static void Register(BundleCollection bundles)
		{
			//frameworks css
			bundles.Add(new StyleBundle("~/css/bootstrap.css")
				.Include("~/Content/bootstrap.css"));
			bundles.Add(new StyleBundle("~/css/bootstrap-theme.css")
				.Include("~/Content/bootstrap-theme.css"));

			//frameworks js
			bundles.Add(new ScriptBundle("~/js/jquery.js")
				.Include("~/Scripts/jquery-{version}.js"));
			bundles.Add(new ScriptBundle("~/js/bootstrap.js")
				.Include("~/Scripts/bootstrap.js"));
			bundles.Add(new ScriptBundle("~/js/knockout.js")
				.Include("~/Scripts/knockout-{version}.js"));
			bundles.Add(new ScriptBundle("~/js/require.js")
				.Include("~/Scripts/require.js"));
			bundles.Add(new ScriptBundle("~/js/sammy.js")
				.Include("~/Scripts/sammy-{version}.js"));

			//website css
			bundles.Add(new StyleBundle("~/css/site.css")
				.Include("~/css/site.css"));
			bundles.Add(new StyleBundle("~/css/user.css")
				.Include("~/css/user.css"));

			//website js
			bundles.Add(new ScriptBundle("~/app/app.loader.js")
				.Include("~/app/app.loader.js"));
			bundles.Add(new ScriptBundle("~/app/app.js")
				.Include("~/app/app.js"));


			bundles.Add(new ScriptBundle("~/XD.js")
				.Include("~/Scripts/bootstrap.js")
				.Include("~/app/app.loader.js")
				.Include("~/app/app.js"));
		}
	}
}