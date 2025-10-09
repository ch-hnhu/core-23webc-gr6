using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Data;
using core_23webc_gr6.Models;
using MySql.Data.MySqlClient;
using core_23webc_gr6.Data.Seeds; //VqNam Sửa toàn bộ trong productcontroller 7/10

namespace core_23webc_gr6.Controllers
{
	public class ProductController : Controller
	{
		private readonly DatabaseHelper _db;

		public ProductController(DatabaseHelper db)
		{
			_db = db;
		}

		// Load danh sách sản phẩm
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
		public ActionResult Details(int id = 1)
		{
			ViewData["BigTitle"] = "Product Detail";
			return View();
		}
	}
}
