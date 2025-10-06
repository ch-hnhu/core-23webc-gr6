using core_23webc_gr6.Models;

namespace core_23webc_gr6.Interfaces
{

    //LTMKieu
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product? GetProductById(int id);
        void AddProduct(Product product);
        void SetProducts(List<Product> products); // Thêm method này cho middleware
    }
    //endLTMKieu
}
