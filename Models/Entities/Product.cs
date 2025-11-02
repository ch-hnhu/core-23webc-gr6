using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core_23webc_gr6.Models.Entities;

public partial class Product
{
	[Key]
	[Column("productId")]
	public int ProductId { get; set; }

	[Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
	[StringLength(250, ErrorMessage = "Tên sản phẩm không được vượt quá 250 ký tự")]
	[Column("productName")]
	[Display(Name = "Tên sản phẩm")]
	public string ProductName { get; set; } = null!;

	[Required(ErrorMessage = "Danh mục là bắt buộc")]
	[Column("categoryId")]
	[Display(Name = "Danh mục")]
	public int CategoryId { get; set; }

	[Required(ErrorMessage = "Giá sản phẩm là bắt buộc")]
	[Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
	[Column("price", TypeName = "decimal(18, 2)")]
	[Display(Name = "Giá")]
	[DisplayFormat(DataFormatString = "{0:N0} ₫")]
	public decimal Price { get; set; }

	[Range(0, 100, ErrorMessage = "Phần trăm giảm giá phải từ 0 đến 100")]
	[Column("discountPercentage")]
	[Display(Name = "Giảm giá (%)")]
	public int DiscountPercentage { get; set; }

	[Required(ErrorMessage = "Số lượng tồn kho là bắt buộc")]
	[Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho phải lớn hơn hoặc bằng 0")]
	[Column("stock")]
	[Display(Name = "Tồn kho")]
	public int Stock { get; set; }

	[Column("images")]
	[Display(Name = "Hình ảnh")]
	public string? Images { get; set; }

	[Column("description")]
	[Display(Name = "Mô tả")]
	[DataType(DataType.MultilineText)]
	public string? Description { get; set; }

	[Column("status")]
	[Display(Name = "Trạng thái")]
	public bool Status { get; set; }

	[Column("views")]
	[Display(Name = "Lượt xem")]
	public int? Views { get; set; }

	[Column("createdAt")]
	[Display(Name = "Ngày tạo")]
	[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
	public DateTime CreatedAt { get; set; }

	[Column("updatedAt")]
	[Display(Name = "Ngày cập nhật")]
	[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
	public DateTime UpdatedAt { get; set; }

	[Display(Name = "Chi tiết hóa đơn")]
	public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

	[Display(Name = "Giỏ hàng")]
	public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

	[ForeignKey("CategoryId")]
	[Display(Name = "Danh mục")]
	public virtual Category Category { get; set; } = null!;

	[Display(Name = "Đánh giá")]
	public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

	[Display(Name = "Thẻ tag")]
	public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
