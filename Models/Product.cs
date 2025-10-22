using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using core_23webc_gr6.Helper;

namespace core_23webc_gr6.Models
{
	//LTMKieu
	public class Product
	{
		public int productId { get; set; }              // Khóa chính
		public string productName { get; set; } = string.Empty;      // Tên sản phẩm
		public int? categoryId { get; set; }            // Mã danh mục (có thể null)
		public decimal price { get; set; }              // Giá
		public int discountPercentage { get; set; }     // Phần trăm giảm giá
		public int stock { get; set; }                  // Số lượng tồn kho
		public string? image { get; set; } = string.Empty;           // Ảnh
		public string? description { get; set; } = string.Empty;     // Mô tả
		public byte status { get; set; }                // Trạng thái (1 = hoạt động, 0 = ẩn)
		public DateTime createdAt { get; set; }         // Ngày tạo
		public DateTime updatedAt { get; set; }

		// PNSon - Thuộc tính bổ sung từ JOIN cho Details view
		public string? categoryName { get; set; }
		public List<string>? Tag { get; set; } = new List<string>();

		//LTMKieu 18/10/2025

		public List<Product> GetAllProducts(DatabaseHelper db)
		{
			var products = new List<Product>();
			using (var connection = db.GetConnection())
			{
				connection.Open();

				string query = "SELECT * FROM Products";
				using (var comd = new SqlCommand(query, connection))
				using (var reader = comd.ExecuteReader())
				{
					while (reader.Read())
					{
						var product = new Product
						{
							productId = Convert.ToInt32(reader["productId"]),
							productName = reader["productName"].ToString() ?? string.Empty,
							categoryId = reader["categoryId"] as int?,
							price = Convert.ToDecimal(reader["price"]),
							discountPercentage = Convert.ToInt32(reader["discountPercentage"]),
							stock = Convert.ToInt32(reader["stock"]),
							image = reader["image"].ToString(),
							description = reader["description"].ToString(),
							status = Convert.ToByte(reader["status"]),
							createdAt = Convert.ToDateTime(reader["createdAt"]),
							updatedAt = Convert.ToDateTime(reader["updatedAt"])
						};
						products.Add(product);
					}
				}
			}
			return products;
		}
		public Product? GetProductById(int id, DatabaseHelper db)
		{
			Product? product = null;
			using (var connection = db.GetConnection())
			{
				connection.Open();
				// PNSon 11/10/2025 Sửa query để lấy thêm dữ liệu từ bảng categories và tags
				string query = @"
                    SELECT 
                        p.productId, p.productName, p.categoryId, p.price, 
                        p.discountPercentage, p.stock, p.image, p.description, 
                        p.status, p.createdAt, p.updatedAt,
                        c.categoryName,
                        STRING_AGG(t.tagName, ', ') WITHIN GROUP (ORDER BY t.tagName) AS TagNames
                    FROM products p
                    LEFT JOIN categories c ON p.categoryId = c.categoryId
                    LEFT JOIN producttags pt ON p.productId = pt.productId
                    LEFT JOIN tags t ON pt.tagId = t.tagId
                    WHERE p.productId = @id
                    GROUP BY p.productId, p.productName, p.categoryId, p.price, 
                            p.discountPercentage, p.stock, p.image, p.description, 
                            p.status, p.createdAt, p.updatedAt, c.categoryName;";
				//endPNSon
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@id", id);
					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							product = new Product
							{
								productId = Convert.ToInt32(reader["productId"]),
								productName = reader["productName"].ToString() ?? string.Empty,
								categoryId = reader["categoryId"] as int?,
								price = Convert.ToDecimal(reader["price"]),
								discountPercentage = Convert.ToInt32(reader["discountPercentage"]),
								stock = Convert.ToInt32(reader["stock"]),
								image = reader["image"].ToString(),
								description = reader["description"].ToString(),
								status = Convert.ToByte(reader["status"]),
								createdAt = Convert.ToDateTime(reader["createdAt"]),
								updatedAt = Convert.ToDateTime(reader["updatedAt"]),

								//PNSon 11/10/2025 Các thuộc tính bổ sung từ JOIN
								categoryName = reader["categoryName"]?.ToString(),
								Tag = reader["TagNames"] != DBNull.Value
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
		public List<Product> GetRelatedProducts(int productId, DatabaseHelper db)
		{
			var relatedProducts = new List<Product>();

			using (var connection = db.GetConnection())
			{
				connection.Open();

				string query = @"
                    SELECT TOP 10
                        p.productId, p.productName, p.price, p.discountPercentage, p.image,
                        STRING_AGG(t.tagName, ', ') WITHIN GROUP (ORDER BY t.tagName) AS TagNames
                    FROM products p
                    JOIN producttags pt ON p.productId = pt.productId
                    JOIN tags t ON pt.tagId = t.tagId
                    WHERE t.tagId IN (SELECT tagId FROM producttags WHERE productId = @productId)
                    AND p.productId <> @productId AND p.status = 1
                    GROUP BY p.productId, p.productName, p.price, p.discountPercentage, p.image, p.createdAt
                    ORDER BY p.createdAt DESC;";
				using (var cmd = new SqlCommand(query, connection))
				{
					cmd.Parameters.AddWithValue("@productId", productId);

					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var p = new Product
							{
								productId = Convert.ToInt32(reader["productId"]),
								productName = reader["productName"].ToString() ?? "",
								price = Convert.ToDecimal(reader["price"]),
								discountPercentage = Convert.ToInt32(reader["discountPercentage"]),
								image = reader["image"].ToString(),
								Tag = reader["TagNames"] != DBNull.Value
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
