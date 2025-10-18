using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Models;
using core_23webc_gr6.Helper;

namespace core_23webc_gr6.Controllers
{
	public class HomeController : Controller
	{
		private readonly DatabaseHelper _db;

		public HomeController(DatabaseHelper db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			//CHNhu 12/10/2025
			//  Lấy tất cả sản phẩm từ model Product (SQL Server) và trả về sản phẩm đầu tiên
			//LTMKieu 18/10/2025
			var productsInstance = new Products();
			var products = productsInstance.GetAllProducts(_db);
			//end LTMKieu
			var latestProducts = products?.Take(6).ToList() ?? new List<Products>();
			return View(latestProducts);
			//end CHNhu
		}
	}
}
