namespace core_23webc_gr6.Models.ViewModels
{
    public class CategoryWithProductVM
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Status { get; set; }

        // Sản phẩm đầu tiên để hiển thị hình
        public string? ProductImage { get; set; }
        public string? ProductName { get; set; }
    }
}
