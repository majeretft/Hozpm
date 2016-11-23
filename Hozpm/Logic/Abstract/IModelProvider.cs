using Hozpm.Models;
using Hozpm.Models.Entities;

namespace Hozpm.Logic.Abstract
{
	public interface IModelProvider
	{
		CatalogHomeViewModel GetCatalogHomeViewModel();
		CatalogHomeViewModel GetCatalogHomeViewModel(AsideFormViewModel formSettings);
		bool TryGetProductViewModel(string uri, out ProductViewModel result);
		bool TryGetKitViewModel(string uri, out KitViewModel result);
	}
}
