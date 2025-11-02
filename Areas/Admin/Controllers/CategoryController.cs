// CongVuong
using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Helper;
using Microsoft.Data.SqlClient;
using core_23webc_gr6.Models;

namespace core_23webc_gr6.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController : Controller
	{
		private readonly DatabaseHelper _db;
		public CategoryController(DatabaseHelper db)
		{
			_db = db;
		}
			// GET: CategoryController

		public IActionResult Index(int page = 1, int pageSize = 10)
		{
			// Lấy danh mục
			var categoryV = new Category();
			var categories = categoryV.GetAllCategories(_db);
			// Phân trang
			int total = categories.Count;
			int totalPages = (int)Math.Ceiling((double)total / pageSize);

			var paged = categories.Skip((page - 1) * pageSize).Take(pageSize).ToList();

			if (totalPages == 0) totalPages = 1;
			if (page < 1) page = 1;
			if (page > totalPages) page = totalPages;

			var pagedCategories = categories
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			ViewBag.Page = page;
			ViewBag.TotalPages = totalPages;

			return View(paged);
		}
	}
}
// endCongVuong