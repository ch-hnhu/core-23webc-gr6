// Models/Tag.cs
using System;

//PNSon thêm 11/10/2025
namespace core_23webc_gr6.Models
{
	public class Tags
	{
		public int TagID { get; set; }               // Khóa chính
		public string TagName { get; set; } = "";    // Tên tag
		public string? Description { get; set; }     // Mô tả tag
		public bool Status { get; set; } = true;     // Trạng thái hoạt động
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime UpdatedAt { get; set; } = DateTime.Now;
	}
}
//endPNSon
