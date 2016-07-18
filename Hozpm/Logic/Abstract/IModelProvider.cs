using Hozpm.Models;

namespace Hozpm.Logic.Abstract
{
	public interface IModelProvider
	{
		CatalogHomeViewModel GetCatalogHomeViewModel();
		bool TryGetProductViewModel(string uri, out ProductViewModel result);
		bool TryGetKitViewModel(string uri, out KitViewModel result);
	}
}
