using Microsoft.AspNetCore.Mvc;

namespace core_23webc_gr6.Controllers
{
	public class CartController : Controller
	{
		// GET: CartController
		public IActionResult Index()
		{
			ViewData["BigTitle"] = "Shopping Cart";

			ViewBag.isActive = "Cart/Index";

			return View();
		}

	}
}
