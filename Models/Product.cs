using System.ComponentModel.DataAnnotations;

namespace core_23webc_gr6.Models
{

    //LTMKieu
    public class Product
    {
        public string MaSP { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
        [RegularExpression(@"^[\p{L}\d\s]+$", ErrorMessage = "Tên sản phẩm chỉ được nhập chữ, số và khoảng trắng, không chứa ký tự đặc biệt.")]
        public string TenSP { get; set; } = string.Empty;

        [Range(1, double.MaxValue, ErrorMessage = "Đơn giá phải là số lớn hơn 0.")]
        public decimal DonGia { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá khuyến mãi phải là số không âm.")]
        public decimal DonGiaKhuyenMai { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống.")]
        public string MoTa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Loại sản phẩm không được để trống.")]
        [RegularExpression(@"^[\p{L}\d\s]+$", ErrorMessage = "Loại sản phẩm chỉ được nhập chữ, số và khoảng trắng, không chứa ký tự đặc biệt.")]
        public string LoaiSP { get; set; } = string.Empty;

        public string HinhAnh { get; set; } = string.Empty;
    }
    //endLTMKieu

    //PNSon đã thêm DataAnnotations 4/10/2024 để làm thông báo lỗi cho form AddProduct
}
