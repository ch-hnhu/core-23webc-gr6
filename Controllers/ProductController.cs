using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Data;
using core_23webc_gr6.Models;
using Microsoft.Data.SqlClient;

//VqNam Sửa toàn bộ trong productcontroller 7/10
// CHNhu - 11/10/2025 - Sửa 1 vài phần từ mysql sang mssql
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
				string sql = "SELECT * FROM products";  // tên bảng bạn có trong SQL Server
				using var cmd = new SqlCommand(sql, conn);
				using var reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					products.Add(new Product
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
		public ActionResult Details(int id = 1)
		{
			ViewData["BigTitle"] = "Product Detail";
			return View();
		}
	}
}