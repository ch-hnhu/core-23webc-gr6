// Models/BillDetail.cs
using System;

// PNSon thêm 11/10/2025
namespace core_23webc_gr6.Models
{
	public class BillDetail
	{
		public int billDetailId { get; set; }               // Khóa chính
		public int billId { get; set; }                     // Mã hóa đơn (FK -> Bills)
		public int productId { get; set; }                  // Mã sản phẩm (FK -> Products)
		public int quantity { get; set; } = 1;              // Số lượng
		public decimal unitPrice { get; set; } = 0.00m;     // Đơn giá
		public decimal discountPercentage { get; set; } = 0; // Phần trăm giảm giá
		public decimal subTotal => quantity * unitPrice * (1 - discountPercentage / 100);    // Thành tiền (computed column)
		public DateTime createdAt { get; set; } = DateTime.Now; // Ngày tạo
		public DateTime updatedAt { get; set; } = DateTime.Now; // Ngày cập nhật
	}
}
// endPNSon
