// Models/Contact.cs
using System;

// PNSon thêm 11/10/2025
namespace core_23webc_gr6.Models
{
	public class Contact
	{
		public int contactId { get; set; }              // Khóa chính
		public string name { get; set; } = "";          // Họ tên người liên hệ
		public string email { get; set; } = "";         // Địa chỉ email
		public string? phone { get; set; }              // Số điện thoại
		public string? subject { get; set; }            // Tiêu đề
		public string content { get; set; } = "";       // Nội dung liên hệ
		public bool status { get; set; } = true;        // Trạng thái (đã xử lý/chưa)
		public DateTime createdAt { get; set; } = DateTime.Now; // Ngày tạo
	}
}
// endPNSon
