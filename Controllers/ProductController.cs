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
		//vqNam 3/11 Thêm phân trang
		public IActionResult Index(int page = 1, int pageSize = 8) 
		{
			var productsInstance = new Product();
			var products = productsInstance.GetAllProducts(_db);

			int totalProducts = products.Count;
			int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);


			if (page < 1) page = 1;
			if (page > totalPages) page = totalPages;

		
			var pagedProducts = products
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

	
			ViewBag.Page = page;
			ViewBag.TotalPages = totalPages;

		
			ViewData["BigTitle"] = "Shop";
			ViewBag.isActive = "Product/Index";

			return View(pagedProducts); 
		}
		//endvqNam
		//endvqNam 3-11

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
