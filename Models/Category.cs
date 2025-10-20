// Models/Category.cs
using System;

//PNSon thêm 11/10/2025
namespace core_23webc_gr6.Models
{
	public class Category
	{
		public int categoryId { get; set; }           // Khóa chính
		public string categoryName { get; set; } = ""; // Tên danh mục
		public string? description { get; set; }       // Mô tả danh mục
		public bool status { get; set; } = true;       // Trạng thái hoạt động
		public DateTime createdAt { get; set; } = DateTime.Now; // Ngày tạo
		public DateTime updatedAt { get; set; } = DateTime.Now; // Ngày cập nhật
	}
}
//endPNSon
