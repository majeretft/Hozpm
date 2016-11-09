using System.Web.Optimization;

namespace Hozpm
{
	public class BundleConfig
	{
		public static void Register(BundleCollection bundles)
		{
			//frameworks css
			bundles.Add(new StyleBundle("~/css/bootstrap", "https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.6/css/bootstrap.min.css")
				.Include("~/Content/bootstrap.css"));

			//frameworks js
			bundles.Add(new ScriptBundle("~/js/jquery", "https://cdnjs.cloudflare.com/ajax/libs/jquery/1.12.4/jquery.min.js")
				.Include("~/Scripts/jquery-{version}.js"));
			bundles.Add(new ScriptBundle("~/js/bootstrap", "https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.6/js/bootstrap.min.js")
				.Include("~/Scripts/bootstrap.js"));
			bundles.Add(new ScriptBundle("~/js/knockout", "https://cdnjs.cloudflare.com/ajax/libs/knockout/3.4.0/knockout-min.js")
				.Include("~/Scripts/knockout-{version}.js"));
			bundles.Add(new ScriptBundle("~/js/require", "https://cdnjs.cloudflare.com/ajax/libs/require.js/2.2.0/require.min.js")
				.Include("~/Scripts/require.js"));
			bundles.Add(new ScriptBundle("~/js/sammy", "https://cdnjs.cloudflare.com/ajax/libs/sammy.js/0.7.6/sammy.min.js")
				.Include("~/Scripts/sammy-{version}.js"));

			//website css
			bundles.Add(new StyleBundle("~/css/site")
				.Include("~/css/site.css"));
			bundles.Add(new StyleBundle("~/css/user")
				.Include("~/css/user.css"));
			bundles.Add(new StyleBundle("~/css/ie")
				.Include("~/css/ie.css"));

			// product and kit page plugins
			bundles.Add(new ScriptBundle("~/js/elevatezoom", "https://cdnjs.cloudflare.com/ajax/libs/elevatezoom/3.0.8/jquery.elevatezoom.min.js")
				.Include("~/Scripts/jquery.elevatezoom.js"));
			bundles.Add(new ScriptBundle("~/js/elevatezoom-start")
				.Include("~/Scripts/site/elevatezoom-start.js"));
		}
	}
}