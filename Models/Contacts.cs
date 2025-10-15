// Models/Contact.cs
using System;

// PNSon thêm 11/10/2025
namespace core_23webc_gr6.Models
{
    public class Contacts
    {
        public int ContactId { get; set; }              // Khóa chính
        public string Name { get; set; } = "";          // Họ tên người liên hệ
        public string Email { get; set; } = "";         // Địa chỉ email
        public string? Phone { get; set; }              // Số điện thoại
        public string? Subject { get; set; }            // Tiêu đề
        public string Content { get; set; } = "";       // Nội dung liên hệ
        public bool Status { get; set; } = true;        // Trạng thái (đã xử lý/chưa)
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Ngày tạo
    }
}
// endPNSon
