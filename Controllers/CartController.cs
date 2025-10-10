using Microsoft.AspNetCore.Mvc;

namespace core_23webc_gr6.Controllers
{
	public class CartController : Controller
	{
		// GET: CartController
		public ActionResult Index()
		{
			ViewData["BigTitle"] = "Shopping Cart";

			return View();
		}

	}
}
