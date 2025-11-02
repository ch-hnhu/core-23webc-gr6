// Models/Category.cs
using System;
using Microsoft.Data.SqlClient;
using core_23webc_gr6.Helper;

//PNSon thêm 11/10/2025
namespace core_23webc_gr6.Models
{
	public class Category
	{
		public int categoryId { get; set; }           // Khóa chính
		public string categoryName { get; set; } = ""; // Tên danh mục
		public string? description { get; set; } = string.Empty;      // Mô tả danh mục
		public bool status { get; set; } = true;       // Trạng thái hoạt động
		public DateTime createdAt { get; set; } = DateTime.Now; // Ngày tạo
		public DateTime updatedAt { get; set; } = DateTime.Now; // Ngày cập nhật

		// CHNhu - 01/11/2025 - Lấy tất cả danh mục
		public List<Category> GetAllCategories(DatabaseHelper db)
		{
			var categories = new List<Category>();
			using (var connection = db.GetConnection())
			{
				connection.Open();
				string query = "SELECT * FROM Categories WHERE status = 1 ORDER BY categoryName";
				using (var cmd = new SqlCommand(query, connection))
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						categories.Add(new Category
						{
							categoryId = Convert.ToInt32(reader["categoryId"]),
							categoryName = reader["categoryName"].ToString() ?? "",
							description = reader["description"].ToString(),
							status = Convert.ToBoolean(reader["status"]),
							createdAt = Convert.ToDateTime(reader["createdAt"]),
							updatedAt = Convert.ToDateTime(reader["updatedAt"])
						});
					}
				}
			}
			return categories;
		}
		// endCHNhu
	}
}
//endPNSon
