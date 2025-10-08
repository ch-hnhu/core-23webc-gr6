using core_23webc_gr6.Interfaces;
using core_23webc_gr6.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
namespace core_23webc_gr6.Repositories
{
    public class ProductRepository : IProductRepository
    {
        //LTMKieu 
        //LTMKieu 06/10/2025
        private readonly string _connString;
        public ProductRepository(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("MySqlConnection") ?? "";
        }
        //endLTMKieu 06/10/2025
        //NTNguyen
        private List<Product> _products = new();
        //endNTNguyen
        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            using (var connection = new MySqlConnection(_connString))
            {
                connection.Open();
                string query = "SELECT * FROM products";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductID = reader.GetInt32("ProductID"),
                            ProductName = reader.GetString("ProductName"),
                            CategoryID = reader.GetInt32("CategoryID"),
                            Price = reader.GetDecimal("Price"),
                            DiscountPercentage = reader.GetInt32("DiscountPercentage"),
                            Stock = reader.GetInt32("Stock"),
                            Image = reader.GetString("Image"),
                            Description = reader.GetString("Description"),
                            Status = reader.GetByte("Status"),
                            CreatedAt = reader.GetDateTime("CreatedAt"),
                            UpdatedAt = reader.GetDateTime("UpdatedAt")
                        });
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
                                ProductID = reader.GetInt32("ProductID"),
                                ProductName = reader.GetString("ProductName"),
                                CategoryID = reader.GetInt32("CategoryID"),
                                Price = reader.GetDecimal("Price"),
                                DiscountPercentage = reader.GetInt32("DiscountPercentage"),
                                Stock = reader.GetInt32("Stock"),
                                Image = reader.GetString("Image"),
                                Description = reader.GetString("Description"),
                                Status = reader.GetByte("Status"),
                                CreatedAt = reader.GetDateTime("CreatedAt"),
                                UpdatedAt = reader.GetDateTime("UpdatedAt")
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
