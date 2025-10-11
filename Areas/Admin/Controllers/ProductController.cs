using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Interfaces;

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
		public IActionResult Index()
		{
			var products = _productRepository.GetAllProducts();
			return View(products);
		}
		public IActionResult Details(int id)
		{
			var product = _productRepository.GetProductById(id);
			if (product == null)
			{
				return NotFound();
			}

			ViewData["Area"] = "";// Gửi thông tin area xuống view PNSon thêm để view biết nó đang ở area nào

			return View(product);
		}

	}
}
