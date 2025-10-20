// Models/Review.cs
using System;

// PNSon thêm 11/10/2025
namespace core_23webc_gr6.Models
{
	public class Review
	{
		public int reviewId { get; set; }               // Khóa chính
		public int userId { get; set; }                 // Khóa ngoại -> Users
		public int productId { get; set; }              // Khóa ngoại -> Products
		public string? content { get; set; }            // Nội dung đánh giá
		public byte rating { get; set; } = 5;           // Điểm đánh giá (1–5)
		public bool status { get; set; } = true;        // Trạng thái hiển thị
		public DateTime createdAt { get; set; } = DateTime.Now; // Ngày tạo
		public DateTime updatedAt { get; set; } = DateTime.Now; // Ngày cập nhật

	}
}
// endPNSon
