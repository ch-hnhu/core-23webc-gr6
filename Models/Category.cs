// Models/Category.cs
using System;
using System.Collections.Generic;
using core_23webc_gr6.Helper;
using Microsoft.Data.SqlClient;

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
		public  List<Category> GetAllCategories(DatabaseHelper db)// Vương thêm 2/11/2025
        {
            var categories = new List<Category>();
			using (var connection = db.GetConnection())
			{
				connection.Open();

				string query = "SELECT * FROM Categories";
				using (var comd = new SqlCommand(query, connection))
				using (var reader = comd.ExecuteReader())
				{
					while (reader.Read())
					{
						var category = new Category
						{
							categoryId = Convert.ToInt32(reader["categoryId"]),
							categoryName = reader["categoryName"].ToString() ?? string.Empty,
							description = reader["description"].ToString(),
							status = Convert.ToBoolean(reader["status"]),
							createdAt = Convert.ToDateTime(reader["createdAt"]),
							updatedAt = Convert.ToDateTime(reader["updatedAt"])
						};
						categories.Add(category);
					}
				}
			}
			return categories;
        }

	};
	
}
//endPNSon
