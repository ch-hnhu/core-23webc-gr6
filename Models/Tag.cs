// Models/Tag.cs
using System;

//PNSon thêm 11/10/2025
namespace core_23webc_gr6.Models
{
	public class Tag
	{
		public int tagId { get; set; }               // Khóa chính
		public string tagName { get; set; } = "";    // Tên tag
		public string? description { get; set; }     // Mô tả tag
		public bool status { get; set; } = true;     // Trạng thái hoạt động
		public DateTime createdAt { get; set; } = DateTime.Now;
		public DateTime updatedAt { get; set; } = DateTime.Now;
	}
}
//endPNSon
