// CHNhu
using Microsoft.AspNetCore.Mvc;

namespace core_23webc_gr6.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : Controller
	{
		// GET: HomeController
		public IActionResult Index()
		{
			return View();
		}

	}
}
// endCHNhu