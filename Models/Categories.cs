// Models/Category.cs
using System;




//PNSon thêm 11/10/2025
namespace core_23webc_gr6.Models
{
    public class Categories
    {
        public int CategoryID { get; set; }           // Khóa chính
        public string CategoryName { get; set; } = ""; // Tên danh mục
        public string? Description { get; set; }       // Mô tả danh mục
        public bool Status { get; set; } = true;       // Trạng thái hoạt động
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Ngày tạo
        public DateTime UpdatedAt { get; set; } = DateTime.Now; // Ngày cập nhật
    }
}
//endPNSon
