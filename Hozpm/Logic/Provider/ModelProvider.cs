using Hozpm.Logic.Abstract;
using Hozpm.Models;

namespace Hozpm.Logic.Provider
{
	public class ModelProvider : IModelProvider
	{
		private readonly DataProvider _dataProvider;

		public ModelProvider(DataProvider dataProvider)
		{
			_dataProvider = dataProvider;
		}

		public CatalogHomeViewModel GetCatalogHomeViewModel()
		{
			throw new System.NotImplementedException();
		}

		public ProductViewModel GetProductViewModel(string uri)
		{
			throw new System.NotImplementedException();
		}

		public KitViewModel GetKitViewModel(string uri)
		{
			throw new System.NotImplementedException();
		}
	}
}