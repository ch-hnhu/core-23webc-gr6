using Microsoft.AspNetCore.Mvc;

namespace core_23webc_gr6.Controllers
{
	public class ContactController : Controller
	{
		// GET: ContactController
		public IActionResult Index()
		{
			ViewData["BigTitle"] = "Contact Us";
			return View();
		}

	}
}
