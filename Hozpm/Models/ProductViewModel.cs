using System.Collections.Generic;
using Hozpm.Logic.Entities;

namespace Hozpm.Models
{
	public class ProductViewModel
	{
		public Product Product { get; set; }
		public IEnumerable<Product> AnalogicProducts { get; set; }
	}
}