using Hozpm.Models;

namespace Hozpm.Logic.Interface
{
	public interface IModelProvider
	{
		CatalogHomeViewModel GetCatalogHomeViewModel();
		ProductViewModel GetProductViewModel(string uri);
		KitViewModel GetKitViewModel(string uri);
	}
}
