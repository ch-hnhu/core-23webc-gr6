using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Models;
using core_23webc_gr6.Helper;
using Microsoft.Data.SqlClient;

namespace core_23webc_gr6.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		// CHNhu - 12/10/2025 - Chỉnh lại lấy dssp từ db dùng DatabaseHelper
		private readonly DatabaseHelper _db;
		public ProductController(DatabaseHelper db)
		{
			_db = db;
		}
		public IActionResult Index(int page = 1, int pageSize = 5)
		{
			List<Product> products = new();

			string query = "select * from Products";

			SqlConnection conn = _db.GetConnection();
			conn.Open();
			SqlDataReader dr = _db.ExcuteQuery(query, conn);
			while (dr.Read())
			{
				products.Add(new Product
				{
					productId = Convert.ToInt32(dr["productId"]),
					productName = dr["productName"].ToString() ?? string.Empty,
					categoryId = dr["categoryId"] as int?,
					price = Convert.ToDecimal(dr["price"]),
					discountPercentage = Convert.ToInt32(dr["discountPercentage"]),
					stock = Convert.ToInt32(dr["stock"]),
					image = dr["image"].ToString(),
					description = dr["description"].ToString(),
					status = Convert.ToByte(dr["status"]),
					createdAt = Convert.ToDateTime(dr["createdAt"]),
					updatedAt = Convert.ToDateTime(dr["updatedAt"])
				});
			}
			dr.Close();
			conn.Close();

			int totalProducts = products.Count();

			int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
			if (page < 1) page = 1;
			if (page > totalPages) page = totalPages;

			var pagedProducts = products
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			ViewBag.Page = page;
			ViewBag.TotalPages = totalPages;

			return View(pagedProducts);
		}

		public IActionResult AddProduct()
		{
			return View(new Product());
		}

		// : xử lý thêm sản phẩm ( tạm thời chưa thêm xử lý sau)
	}
}
