using System.Web.Mvc;
using Hozpm.Logic.Abstract;
using MvcSiteMapProvider;

namespace Hozpm.Logic.Sitemap
{
	public abstract class ItemDynamicNodeProvider : DynamicNodeProviderBase
	{
		protected readonly IDataProvider Provider;

		protected ItemDynamicNodeProvider()
		{
			Provider = DependencyResolver.Current.GetService<IDataProvider>();
		}
	}
}