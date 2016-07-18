namespace Hozpm.Logic.Exception
{
	public class ProductItemNotFoundException : System.Exception
	{
		public ProductItemNotFoundException(string message)
			:base(message)
		{
			// nothing
		}
	}
}