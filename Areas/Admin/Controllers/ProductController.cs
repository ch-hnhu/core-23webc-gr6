using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Models;
using core_23webc_gr6.Helper;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace core_23webc_gr6.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		// CHNhu - 12/10/2025 - Chỉnh lại lấy dssp từ db dùng DatabaseHelper
		private readonly DatabaseHelper _db;
		private readonly IWebHostEnvironment _env;
		public ProductController(DatabaseHelper db, IWebHostEnvironment env)
		{
			_db = db;
			_env = env;
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

		// CHNhu - 02/11/2025 - Thêm sản phẩm mới kèm nhiều ảnh
		[HttpGet]
		public IActionResult AddProduct()
		{
			// Load danh sách Category để hiển thị trong dropdown
			var categoryModel = new Category();
			ViewBag.Categories = categoryModel.GetAllCategories(_db);
			return View();
		}

		[HttpPost]
		public IActionResult AddProduct(Product product)
		{
			// Kiểm tra validation
			if (!ModelState.IsValid)
			{
				// Load lại danh sách categories để hiển thị form
				var categoryModel = new Category();
				ViewBag.Categories = categoryModel.GetAllCategories(_db);
				TempData["ErrorMessage"] = "Vui lòng điền đầy đủ thông tin bắt buộc!";
				return View(product);
			}

			try
			{
				int newProductId = product.AddNewProduct(_db);
				if (newProductId > 0)
				{
					List<string> images = new List<string>();
					var uploadDir = Path.Combine(_env.WebRootPath, "img", "Products");
					// Thêm ảnh
					foreach (var file in Request.Form.Files)
					{
						if (file != null && file.Length > 0)
						{
							var ext = Path.GetExtension(file.FileName);
							var name = file.Name; // Lấy tên input file để phân biệt
							var fileName = $"{newProductId}_{name}{ext}";
							var filePath = Path.Combine(uploadDir, fileName);
							// Lưu file
							using (var stream = new FileStream(filePath, FileMode.Create))
							{
								file.CopyTo(stream);
							}
							images.Add(fileName);
						}
					}
					var imagesJson = JsonSerializer.Serialize(images);
					product.productId = newProductId;
					product.images = imagesJson;
					product.UpdateProduct(_db);
					TempData["SuccessMessage"] = "Thêm sản phẩm thành công!";
					// Redirect để xóa toàn bộ input và load form mới
					return RedirectToAction("AddProduct");
				}
				TempData["ErrorMessage"] = "Thêm sản phẩm thất bại!";
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = "Lỗi: " + ex.Message;
				var categoryModel = new Category();
				ViewBag.Categories = categoryModel.GetAllCategories(_db);
				return View(product);
			}

			return View(product);
		}
	}
}
