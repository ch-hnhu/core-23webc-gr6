using core_23webc_gr6.Interfaces;
using core_23webc_gr6.Models;
using MySql.Data.MySqlClient;
using System.Data;
namespace core_23webc_gr6.Repositories
{
    public class ProductRepository : IProductRepository
    {
        //LTMKieu
        //LTMKieu 06/10/2025
        private readonly string _connString = string.Empty;
        private readonly AppConfig _config;
        //endLTMKieu 06/10/2025
        public ProductRepository(AppConfig config, IConfiguration configuration)
        {
            _config = config;
            // Đọc connection từ appsettings.json
            _connString = configuration.GetConnectionString("MySqlConnection");
        }
        //NTNguyen
        private List<Product> _products = new();
        //endNTNguyen
        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            using (var connection = new MySqlConnection(_connString))
            {
                connection.Open();

                string query = "SELECT * FROM Products";
                using (var comd = new MySqlCommand(query, connection))
                using (var reader = comd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Product
                        {
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            ProductName = reader["ProductName"].ToString() ?? string.Empty,
                            CategoryID = reader["CategoryID"] as int?,
                            Price = Convert.ToDecimal(reader["Price"]),
                            DiscountPercentage = Convert.ToInt32(reader["DiscountPercentage"]),
                            Stock = Convert.ToInt32(reader["Stock"]),
                            Image = reader["Image"].ToString(),
                            Description = reader["Description"].ToString(),
                            Status = Convert.ToByte(reader["Status"]),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                            UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                        };
                        products.Add(product);
                    }
                }
            }
            return products;
        }
        public Product? GetProductById(int id)
        {
            Product? product = null;
            using (var connection = new MySqlConnection(_connString))
            {
                connection.Open();
                string query = "SELECT * FROM products WHERE ProductID = @id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new Product
                            {
                                ProductID = Convert.ToInt32(reader["ProductID"]),
                                ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                CategoryID = reader["CategoryID"] as int?,
                                Price = Convert.ToDecimal(reader["Price"]),
                                DiscountPercentage = Convert.ToInt32(reader["DiscountPercentage"]),
                                Stock = Convert.ToInt32(reader["Stock"]),
                                Image = reader["Image"].ToString(),
                                Description = reader["Description"].ToString(),
                                Status = Convert.ToByte(reader["Status"]),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                            };
                        }
                    }
                }
            }
            return product;
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
