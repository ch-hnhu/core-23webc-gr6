//aras/Admin/Controllers/ProductController.cs
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
		//PNSon - 26/10/2025 - Đổi phương thức lấy danh sách sản phẩm và phân trang
		public IActionResult Index(int page = 1, int pageSize = 5)
		{
			var productInstance = new Product();
			var allProducts = productInstance.GetAllProducts(_db);

			int totalProducts = allProducts.Count;
			int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

			if (totalPages == 0) totalPages = 1;
			if (page < 1) page = 1;
			if (page > totalPages) page = totalPages;

			var pagedProducts = allProducts
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			ViewBag.Page = page;
			ViewBag.TotalPages = totalPages;

			ViewData["Area"] = "Admin";
			ViewData["Controller"] = "Product";
			ViewData["Action"] = "Index";


			return View(pagedProducts);
		}
		//endPNSon

		public IActionResult AddProduct()
		{
			return View(new Product());
		}

		// : xử lý thêm sản phẩm ( tạm thời chưa thêm xử lý sau)
	}
}
