using System.Collections.Generic;
using MvcSiteMapProvider;

namespace Hozpm.Logic.Sitemap
{
	public class KitDynamicNodeProvider : ItemDynamicNodeProvider
	{
		public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
		{
			foreach (var p in Provider.GetKits())
			{
				var dynamicNode = new DynamicNode
				{
					Title = p.Caption,
					ParentKey = "CatalogController"
				};
				dynamicNode.RouteValues.Add("item", p.Uri);

				yield return dynamicNode;
			}
		}
	}
}