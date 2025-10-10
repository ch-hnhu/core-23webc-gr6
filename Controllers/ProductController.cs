using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Data;
using core_23webc_gr6.Models;
using MySql.Data.MySqlClient;
using core_23webc_gr6.Data.Seeds; //VqNam Sửa toàn bộ trong productcontroller 7/10
using core_23webc_gr6.Repositories; //PNSon 8/10/2025 thêm productRepository
using core_23webc_gr6.Interfaces; 

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
		public ActionResult Index()
		{
			List<Product> products = new();

			using (var conn = _db.GetConnection())
			{
				conn.Open();
				string sql = "SELECT * FROM products";  // tên bảng bạn có trong MySQL
				using var cmd = new MySqlCommand(sql, conn);
				using var reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					products.Add(new Product
					{
						ProductID = reader.GetInt32("ProductID"),
						ProductName = reader.GetString("ProductName"),
						CategoryID = reader.GetInt32("CategoryID"),
						Price = reader.GetDecimal("Price"),
						DiscountPercentage = reader.GetInt32("DiscountPercentage"),
						Stock = reader.GetInt32("Stock"),
						Image = reader.GetString("Image"),
						Description = reader.GetString("Description"),
						Status = reader.GetByte("Status"),
						CreatedAt = reader.GetDateTime("CreatedAt"),
						UpdatedAt = reader.GetDateTime("UpdatedAt")
					});
				}
			}

			ViewData["BigTitle"] = "Shop";
			return View(products); // Trả ra list cho Index.cshtml
		}
		//endvqNam

		//PNSon 8/10/2025 Load chi tiết sản phẩm
		public ActionResult Details(int id)
		{
			var product = _productRepository.GetProductById(id);
			// Kiểm tra null để tránh lỗi Model null trong View
			if (product == null)
			{
				return NotFound("Sản phẩm không tồn tại hoặc đã bị xóa.");
			}
			ViewData["BigTitle"] = "Product Detail";
			return View(product);
		}
		//endPNSon
	}



}
