using System.Collections.Generic;
using Hozpm.Logic.Entities;

namespace Hozpm.Models
{
	public class KitViewModel
	{
		public Kit Kit { get; set; }
		public IEnumerable<ProductBase> IncludedProducts { get; set; }
	}
}