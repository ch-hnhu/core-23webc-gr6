using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Models;
using core_23webc_gr6.Interfaces;

namespace core_23webc_gr6.Controllers
{
	public class HomeController : Controller
	{
		private readonly IProductRepository _productRepository;

		public HomeController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public IActionResult Index()
		{
			//  Lấy tất cả sản phẩm từ repository (SQL Server) và trả về sản phẩm đầu tiên
			var products = _productRepository.GetAllProducts();
			var latestProducts = products?.Take(6).ToList() ?? new List<Product>();
			return View(latestProducts);
		}
	}
}
