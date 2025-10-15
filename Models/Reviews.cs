// Models/Review.cs
using System;

// PNSon thêm 11/10/2025
namespace core_23webc_gr6.Models
{
    public class Reviews
    {
        public int ReviewId { get; set; }               // Khóa chính
        public int UserId { get; set; }                 // Khóa ngoại -> Users
        public int ProductId { get; set; }              // Khóa ngoại -> Products
        public string? Content { get; set; }            // Nội dung đánh giá
        public byte Rating { get; set; } = 5;           // Điểm đánh giá (1–5)
        public bool Status { get; set; } = true;        // Trạng thái hiển thị
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Ngày tạo
        public DateTime UpdatedAt { get; set; } = DateTime.Now; // Ngày cập nhật

    }
}
// endPNSon
