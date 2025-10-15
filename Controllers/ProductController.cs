using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Helper;
using core_23webc_gr6.Models;
using Microsoft.Data.SqlClient;
using core_23webc_gr6.Interfaces;

//VqNam Sửa toàn bộ trong productcontroller 7/10
// CHNhu - 11/10/2025 - Sửa 1 vài phần từ mysql sang mssql
namespace core_23webc_gr6.Controllers
{
	public class ProductController : Controller
	{
		private readonly DatabaseHelper _db;
		private readonly IProductRepository _productRepository; //PNSon 8/10/2025 thêm productRepository

		public ProductController(DatabaseHelper db, IProductRepository productRepository)
		{
			//PNSon 8/10/2025 thêm productRepository để lấy dữ liệu cho trang details
			_productRepository = productRepository;
			//endPNSon
			_db = db;
		}

		//vqNam Load danh sách sản phẩm
		public IActionResult Index()
		{
			List<Products> products = new();

			using (var conn = _db.GetConnection())
			{
				conn.Open();
				string sql = "SELECT * FROM products";  // tên bảng bạn có trong SQL Server
				using var cmd = new SqlCommand(sql, conn);
				using var reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					products.Add(new Products
					{
						ProductID = Convert.ToInt32(reader["ProductID"]),
						ProductName = reader["ProductName"]?.ToString() ?? "",
						CategoryID = Convert.ToInt32(reader["CategoryID"]),
						Price = Convert.ToDecimal(reader["Price"]),
						DiscountPercentage = Convert.ToInt32(reader["DiscountPercentage"]),
						Stock = Convert.ToInt32(reader["Stock"]),
						Image = reader["Image"]?.ToString() ?? "",
						Description = reader["Description"]?.ToString() ?? "",
						Status = Convert.ToByte(reader["Status"]),
						CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
						UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
					});
				}
			}

			ViewData["BigTitle"] = "Shop";
			return View(products); // Trả ra list cho Index.cshtml
		}
		//endvqNam

		//PNSon 8/10/2025 Load chi tiết sản phẩm
		public IActionResult Details(int id)
		{
			var product = _productRepository.GetProductById(id);
			// Kiểm tra null để tránh lỗi Model null trong View
			if (product == null)
			{
				return NotFound("Sản phẩm không tồn tại hoặc đã bị xóa.");
			}

			//PNSon 11/10/2025 Lấy danh sách sản phẩm liên quan dựa trên CategoryID
			var relatedProducts = _productRepository.GetRelatedProducts(id);
			ViewBag.RelatedProducts = relatedProducts;

			ViewData["BigTitle"] = "Product Detail";
			return View(product);
		}
		//endPNSon
	}
}
