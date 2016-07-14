using System.Collections.Generic;

namespace Hozpm.Logic.Entities
{
	public class Kit : ProductBase
	{
		public IEnumerable<int> ProductsIncluded { get; set; } 

		public override bool GetIsKit => true;
	}
}