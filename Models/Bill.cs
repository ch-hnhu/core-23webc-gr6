// Models/Bill.cs
using System;

// PNSon thêm 11/10/2025
namespace core_23webc_gr6.Models
{
	public class Bill
	{
		public int billId { get; set; }                         // Khóa chính
		public int userId { get; set; }                         // Mã người dùng (FK -> Users)
		public string address { get; set; } = "";               // Địa chỉ giao hàng
		public string phone { get; set; } = "";                 // Số điện thoại liên hệ
		public string paymentMethod { get; set; } = "Cash on Delivery"; // Phương thức thanh toán
		public string paymentStatus { get; set; } = "unpaid";   // Trạng thái thanh toán
		public string shippingMethod { get; set; } = "express"; // Phương thức giao hàng
		public int discountPercentage { get; set; } = 0;        // Phần trăm giảm giá
		public decimal totalAmount { get; set; } = 0.00m;       // Tổng tiền
		public bool status { get; set; } = true;                // Trạng thái hóa đơn (hiển thị/ẩn)
		public string deliveryStatus { get; set; } = "pending"; // Trạng thái giao hàng
		public DateTime createdAt { get; set; } = DateTime.Now; // Ngày tạo
		public DateTime updatedAt { get; set; } = DateTime.Now; // Ngày cập nhật
	}
}
// endPNSon
