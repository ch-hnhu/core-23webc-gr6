using core_23webc_gr6.Interfaces;
using core_23webc_gr6.Models;
namespace core_23webc_gr6.Repositories
{
    public class ProductRepository : IProductRepository
    {
        //LTMKieu
        //NTNguyen
        private List<Product> _products = new();
        //endNTNguyen
        public List<Product> GetAllProducts()
        {
            return _products;
        }
        public Product? GetProductById(string id)
        {
            return _products.FirstOrDefault(p => p.MaSP == id);
        }
        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        //NTNguyen
        public void SetProducts(List<Product> products)
        {
            _products.Clear();
            _products.AddRange(products);
        }
        //endNTNguyen
        //endLTMKieu
    
    }
}
