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
                // PNSon 11/10/2025 Sửa query để lấy thêm dữ liệu từ bảng categories và tags
                string query = @"
                    SELECT 
                        p.ProductID, p.ProductName, p.CategoryID, p.Price, 
                        p.DiscountPercentage, p.Stock, p.Image, p.Description, 
                        p.Status, p.CreatedAt, p.UpdatedAt,
                        c.CategoryName,
                        GROUP_CONCAT(t.TagName ORDER BY t.TagName SEPARATOR ', ') AS TagNames
                    FROM products p
                    LEFT JOIN categories c ON p.CategoryID = c.CategoryID
                    LEFT JOIN producttags pt ON p.ProductID = pt.ProductID
                    LEFT JOIN tags t ON pt.TagID = t.TagID
                    WHERE p.ProductID = @id
                    GROUP BY p.ProductID, c.CategoryName;";
                //endPNSon
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
                                UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]),

                                //PNSon 11/10/2025 Các thuộc tính bổ sung từ JOIN
                                CategoryName = reader["CategoryName"]?.ToString(),
                                Tags = reader["TagNames"] != DBNull.Value
                                    ? reader["TagNames"].ToString()!.Split(',')
                                        .Select(t => t.Trim())
                                        .Where(t => !string.IsNullOrEmpty(t))
                                        .ToList()
                                    : new List<string>()
                                //endPNSon
                            };
                        }
                    }
                }
            }
            return product;
        }

        // PNSon 11/10/2025 thêm hàm truy vấn sản phẩm liên quan
        public List<Product> GetRelatedProducts(int productId)
        {
            var relatedProducts = new List<Product>();

            using (var connection = new MySqlConnection(_connString))
            {
                connection.Open();

                string query = @"
            SELECT DISTINCT 
                p.ProductID, p.ProductName, p.Price, p.DiscountPercentage, p.Image,
                GROUP_CONCAT(t.TagName ORDER BY t.TagName SEPARATOR ', ') AS TagNames
            FROM products p
            INNER JOIN producttags pt ON p.ProductID = pt.ProductID
            INNER JOIN tags t ON pt.TagID = t.TagID
            WHERE t.TagID IN (
                SELECT TagID FROM producttags WHERE ProductID = @productId
            )
            AND p.ProductID <> @productId
            AND p.Status = 1
            GROUP BY p.ProductID
            ORDER BY p.CreatedAt DESC
            LIMIT 10;";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@productId", productId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var p = new Product
                            {
                                ProductID = Convert.ToInt32(reader["ProductID"]),
                                ProductName = reader["ProductName"].ToString() ?? "",
                                Price = Convert.ToDecimal(reader["Price"]),
                                DiscountPercentage = Convert.ToInt32(reader["DiscountPercentage"]),
                                Image = reader["Image"].ToString(),
                                Tags = reader["TagNames"] != DBNull.Value
                                    ? reader["TagNames"].ToString()!.Split(',')
                                        .Select(t => t.Trim())
                                        .ToList()
                                    : new List<string>()
                            };
                            relatedProducts.Add(p);
                        }
                    }
                }
            }

            return relatedProducts;
        }
        //endPNSon


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
