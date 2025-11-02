// CHNhu
using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Helper;
using Microsoft.Data.SqlClient;
using core_23webc_gr6.Models;
using core_23webc_gr6.Models.ViewModels;

namespace core_23webc_gr6.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : Controller
	{
		private readonly DatabaseHelper _db;
		public HomeController(DatabaseHelper db)
		{
			_db = db;
		}
			// GET: HomeController
		public IActionResult index(int page = 1, int pageSize = 5)
		{
			// Lấy danh mục
			var categories = Category.GetAllCategories(_db);

			// Lấy danh sách sản phẩm để ghép hình
			var productModel = new Product();
			var products = productModel.GetAllProducts(_db);

			// Gộp danh mục với sản phẩm đầu tiên thuộc danh mục đó
			var categoryVMs = categories.Select(v =>
			{
				var product = products.FirstOrDefault(p => p.categoryId == v.categoryId);
				return new CategoryWithProductVM
				{
					CategoryId = v.categoryId,
					CategoryName = v.categoryName,
					Description = v.description,
					CreatedAt = v.createdAt,
					UpdatedAt = v.updatedAt,
					Status = v.status,
					ProductImage = product?.image,
					ProductName = product?.productName
				};
			}).ToList();

			// Phân trang
			int total = categoryVMs.Count;
			int totalPages = (int)Math.Ceiling((double)total / pageSize);

			var paged = categoryVMs.Skip((page - 1) * pageSize).Take(pageSize).ToList();

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
// endCHNhu