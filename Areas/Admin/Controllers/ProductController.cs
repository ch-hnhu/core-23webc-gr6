using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Interfaces;
using core_23webc_gr6.Models;
using core_23webc_gr6.Repositories;

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

        // Hiển thị danh sách sản phẩm với phân trang (9 sản phẩm mỗi trang)
        public IActionResult Index(int page = 1)
        {
            int pageSize = 9;
            var allProducts = _productRepository.GetAllProducts();

            int totalProducts = allProducts.Count;
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            var pagedProducts = allProducts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Area = "Admin";
            return View(pagedProducts);
        }

        // Dùng lại trang Details.cshtml gốc
        public IActionResult Details(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
                return NotFound();
            ViewBag.Area = "Admin";
            return View("~/Views/Product/Details.cshtml", product);

        }
        // GET: trang thêm sản phẩm
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View(new Product());
        }

        // POST: xử lý thêm sản phẩm ( tạm thời chưa thêm xử lý sau)
    }
}
