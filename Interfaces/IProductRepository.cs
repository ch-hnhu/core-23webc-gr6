using core_23webc_gr6.Models;

namespace core_23webc_gr6.Interfaces
{

	//LTMKieu
	public interface IProductRepository
	{
		List<Products> GetAllProducts();
		Products? GetProductById(int id);
		void AddProduct(Products product);
		void SetProducts(List<Products> products); // Thêm method này cho middleware
	}
	//endLTMKieu
}
