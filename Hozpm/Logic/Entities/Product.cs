namespace Hozpm.Logic.Entities
{
	public class Product : ProductBase
	{
		public int AnalogyId { get; set; }

		public override bool GetIsKit => false;
	}
}