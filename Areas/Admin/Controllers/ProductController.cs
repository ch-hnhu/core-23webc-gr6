using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Interfaces;
using core_23webc_gr6.Models;

namespace core_23webc_gr6.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IProductRepository _productRepository;
		public ProductController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}
		public IActionResult Index(int page = 1, int pageSize = 5)
		{
			var allProducts = _productRepository.GetAllProducts();
			int totalProducts = allProducts.Count();

			int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
			if (page < 1) page = 1;
			if (page > totalPages) page = totalPages;

			var pagedProducts = allProducts
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			ViewBag.Page = page;
			ViewBag.TotalPages = totalPages;

			return View(pagedProducts);
		}

		public IActionResult Details(int id)
		{
			var product = _productRepository.GetProductById(id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}
		public IActionResult AddProduct()
		{
			return View(new Product());
		}

		// : xử lý thêm sản phẩm ( tạm thời chưa thêm xử lý sau)
	}
}
