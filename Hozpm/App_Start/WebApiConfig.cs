using System.Web.Http;

namespace Hozpm
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(null, "api/{controller}/{action}");

			var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
			formatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
		}
	}
}