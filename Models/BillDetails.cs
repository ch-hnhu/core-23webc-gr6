// Models/BillDetail.cs
using System;

// PNSon thêm 11/10/2025
namespace core_23webc_gr6.Models
{
    public class BillDetails
    {
        public int BillDetailId { get; set; }               // Khóa chính
        public int BillId { get; set; }                     // Mã hóa đơn (FK -> Bills)
        public int ProductId { get; set; }                  // Mã sản phẩm (FK -> Products)
        public int Quantity { get; set; } = 1;              // Số lượng
        public decimal UnitPrice { get; set; } = 0.00m;     // Đơn giá
        public decimal SubTotal => Quantity * UnitPrice;    // Thành tiền (computed column)
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Ngày tạo
        public DateTime UpdatedAt { get; set; } = DateTime.Now; // Ngày cập nhật
    }
}
// endPNSon
