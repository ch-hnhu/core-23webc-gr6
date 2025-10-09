using System.ComponentModel.DataAnnotations;

namespace core_23webc_gr6.Models
{

    //LTMKieu
    public class Product
    {
        public int ProductID { get; set; }              // Khóa chính
        public string ProductName { get; set; } = string.Empty;      // Tên sản phẩm
        public int? CategoryID { get; set; }            // Mã danh mục (có thể null)
        public decimal Price { get; set; }              // Giá
        public int DiscountPercentage { get; set; }     // Phần trăm giảm giá
        public int Stock { get; set; }                  // Số lượng tồn kho
        public string? Image { get; set; } = string.Empty;           // Ảnh
        public string? Description { get; set; } = string.Empty;     // Mô tả
        public byte Status { get; set; }                // Trạng thái (1 = hoạt động, 0 = ẩn)
        public DateTime CreatedAt { get; set; }         // Ngày tạo
        public DateTime UpdatedAt { get; set; }
    }
    //endLTMKieu
}
