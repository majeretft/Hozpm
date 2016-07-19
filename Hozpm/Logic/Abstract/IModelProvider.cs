using Hozpm.Logic.Entities;
using Hozpm.Models;

namespace Hozpm.Logic.Abstract
{
	public interface IModelProvider
	{
		CatalogHomeViewModel GetCatalogHomeViewModel();
		CatalogHomeViewModel GetCatalogHomeViewModel(FormSettings formSettings);
		bool TryGetProductViewModel(string uri, out ProductViewModel result);
		bool TryGetKitViewModel(string uri, out KitViewModel result);
	}
}
