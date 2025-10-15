// Models/Bill.cs
using System;

// PNSon thêm 11/10/2025
namespace core_23webc_gr6.Models
{
    public class Bills
    {
        public int BillId { get; set; }                         // Khóa chính
        public int UserId { get; set; }                         // Mã người dùng (FK -> Users)
        public string Address { get; set; } = "";               // Địa chỉ giao hàng
        public string Phone { get; set; } = "";                 // Số điện thoại liên hệ
        public string PaymentMethod { get; set; } = "Cash on Delivery"; // Phương thức thanh toán
        public string PaymentStatus { get; set; } = "unpaid";   // Trạng thái thanh toán
        public string ShippingMethod { get; set; } = "express"; // Phương thức giao hàng
        public int DiscountPercentage { get; set; } = 0;        // Phần trăm giảm giá
        public decimal TotalAmount { get; set; } = 0.00m;       // Tổng tiền
        public bool Status { get; set; } = true;                // Trạng thái hóa đơn (hiển thị/ẩn)
        public string DeliveryStatus { get; set; } = "pending"; // Trạng thái giao hàng
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Ngày tạo
        public DateTime UpdatedAt { get; set; } = DateTime.Now; // Ngày cập nhật
    }
}
// endPNSon
