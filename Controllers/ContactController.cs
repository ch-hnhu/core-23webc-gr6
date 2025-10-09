using Microsoft.AspNetCore.Mvc;

namespace core_23webc_gr6.Controllers
{
	public class ContactController : Controller
	{
		// GET: ContactController
		public ActionResult Index()
		{
			ViewData["BigTitle"] = "Contact Us";
			return View();
		}

	}
}
