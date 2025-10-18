using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using core_23webc_gr6.Helper;

namespace core_23webc_gr6.Models
{
	//LTMKieu
	public class Products
	{
		public int ProductID { get; set; }              // Khóa chính
		public string ProductName { get; set; } = string.Empty;      // Tên sản phẩm
		public int? CategoryID { get; set; }            // Mã danh mục (có thể null)
		public decimal Price { get; set; }              // Giá
		public int DiscountPercentage { get; set; }     // Phần trăm giảm giá
		public int Stock { get; set; }                  // Số lượng tồn kho
		public string? Image { get; set; } = string.Empty;           // Ảnh
		public string? Description { get; set; } = string.Empty;     // Mô tả
		public byte Status { get; set; }                // Trạng thái (1 = hoạt động, 0 = ẩn)
		public DateTime CreatedAt { get; set; }         // Ngày tạo
		public DateTime UpdatedAt { get; set; }

		// PNSon - Thuộc tính bổ sung từ JOIN cho Details view
		public string? CategoryName { get; set; }
		public List<string>? Tags { get; set; } = new List<string>();

		//LTMKieu 18/10/2025
		
		public List<Products> GetAllProducts(DatabaseHelper db)
		{
			var products = new List<Products>();
			using (var connection = db.GetConnection())
			{
				connection.Open();

				string query = "SELECT * FROM Products";
				using (var comd = new SqlCommand(query, connection))
				using (var reader = comd.ExecuteReader())
				{
					while (reader.Read())
					{
						var product = new Products
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
		public Products? GetProductById(int id, DatabaseHelper db)
		{
			Products? product = null;
			using (var connection = db.GetConnection())
			{
				connection.Open();
				// PNSon 11/10/2025 Sửa query để lấy thêm dữ liệu từ bảng categories và tags
				string query = @"
                    SELECT 
                        p.ProductID, p.ProductName, p.CategoryID, p.Price, 
                        p.DiscountPercentage, p.Stock, p.Image, p.Description, 
                        p.Status, p.CreatedAt, p.UpdatedAt,
                        c.CategoryName,
                        STRING_AGG(t.TagName, ', ') WITHIN GROUP (ORDER BY t.TagName) AS TagNames
                    FROM products p
                    LEFT JOIN categories c ON p.CategoryID = c.CategoryID
                    LEFT JOIN producttags pt ON p.ProductID = pt.ProductID
                    LEFT JOIN tags t ON pt.TagID = t.TagID
                    WHERE p.ProductID = @id
                    GROUP BY p.ProductID, p.ProductName, p.CategoryID, p.Price, 
                            p.DiscountPercentage, p.Stock, p.Image, p.Description, 
                            p.Status, p.CreatedAt, p.UpdatedAt, c.CategoryName;";
				//endPNSon
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@id", id);
					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							product = new Products
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
		public List<Products> GetRelatedProducts(int productId, DatabaseHelper db)
		{
			var relatedProducts = new List<Products>();

			using (var connection = db.GetConnection())
			{
				connection.Open();

				string query = @"
                    SELECT TOP 10
                        p.ProductID, p.ProductName, p.Price, p.DiscountPercentage, p.Image,
                        STRING_AGG(t.TagName, ', ') WITHIN GROUP (ORDER BY t.TagName) AS TagNames
                    FROM products p
                    JOIN producttags pt ON p.ProductID = pt.ProductID
                    JOIN tags t ON pt.TagID = t.TagID
                    WHERE t.TagID IN (SELECT TagID FROM producttags WHERE ProductID = @productId)
                    AND p.ProductID <> @productId AND p.Status = 1
                    GROUP BY p.ProductID, p.ProductName, p.Price, p.DiscountPercentage, p.Image, p.CreatedAt
                    ORDER BY p.CreatedAt DESC;";
				using (var cmd = new SqlCommand(query, connection))
				{
					cmd.Parameters.AddWithValue("@productId", productId);

					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var p = new Products
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
		//endLTMKieu
	}
	//endLTMKieu
}
