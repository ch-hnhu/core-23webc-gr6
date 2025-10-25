//Controllers/ProductController.cs
using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Helper;
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

		//vqNam Load danh sách sản phẩm
		public IActionResult Index()
		{
			var productsInstance = new Product();
			var products = productsInstance.GetAllProducts(_db);
			ViewData["BigTitle"] = "Shop";
			return View(products); // Trả ra list cho Index.cshtml
		}
		//endvqNam

		//PNSon 8/10/2025 Load chi tiết sản phẩm
		public IActionResult Details(int id)
		{
			var productsInstance = new Product();
			var product = productsInstance.GetProductById(id, _db);
			// Kiểm tra null để tránh lỗi Model null trong View
			if (product == null)
			{
				return NotFound("Sản phẩm không tồn tại hoặc đã bị xóa.");
			}

			//PNSon 11/10/2025 Lấy danh sách sản phẩm liên quan dựa trên categoryId
			var relatedProducts = productsInstance.GetRelatedProducts(id, _db);
			ViewBag.RelatedProducts = relatedProducts;

			ViewData["BigTitle"] = "Product Detail";
			return View(product);
		}
		//endPNSon
	}
}
