// CHNhu
using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Models;

namespace core_23webc_gr6.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
// endCHNhu