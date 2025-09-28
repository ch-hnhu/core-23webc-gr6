using core_23webc_gr6.Interfaces;
using core_23webc_gr6.Models;
namespace core_23webc_gr6.Repositories
{
    public class ProductRepository : IProductRepository
    {
        //LTMKieu
        //private readonly List<Product> _products;
        //public ProductRepository()
        //{
        //    // Khởi tạo danh sách sản phẩm rong
        //    _products = new List<Product>();
        //}
        private readonly List<Product> _products = new()
        {
            new Product { MaSP = "Ma1", TenSP = "Laptop", DonGia = 1200 },
            new Product { MaSP = "Ma2", TenSP = "Smartphone", DonGia = 800 }
        };
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
        //endLTMKieu
    
    }
}
