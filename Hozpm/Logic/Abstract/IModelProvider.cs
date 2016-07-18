using Hozpm.Models;

namespace Hozpm.Logic.Abstract
{
	public interface IModelProvider
	{
		CatalogHomeViewModel GetCatalogHomeViewModel();
		ProductViewModel GetProductViewModel(string uri);
		KitViewModel GetKitViewModel(string uri);
	}
}
