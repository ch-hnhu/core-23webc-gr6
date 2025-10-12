// Models/User.cs
using System;

// PNSon thêm 11/10/2025
namespace core_23webc_gr6.Models
{
    public class Users
    {
        public int UserId { get; set; }                 // Khóa chính
        public string Username { get; set; } = "";      // Tên đăng nhập
        public string Email { get; set; } = "";         // Địa chỉ email (duy nhất)
        public string Password { get; set; } = "";      // Mật khẩu (đã mã hóa)
        public string Role { get; set; } = "User";      // Vai trò (User/Admin)
        public bool Status { get; set; } = true;        // Trạng thái hoạt động
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Ngày tạo
        public DateTime UpdatedAt { get; set; } = DateTime.Now; // Ngày cập nhật
    }
}
// endPNSon
