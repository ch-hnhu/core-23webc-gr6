// CHNhu - 12/10/2025
using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Models;
using core_23webc_gr6.Helper;

namespace core_23webc_gr6.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AuthController : Controller
	{
		private readonly DatabaseHelper _dbHelper;

		public AuthController(DatabaseHelper dbHelper)
		{
			_dbHelper = dbHelper;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(string username, string password)
		{
			var user = Users.GetUser(username, password, _dbHelper);

			if (user != null && user.role == "Admin")
			{
				return RedirectToAction("Index", "Home", new { area = "Admin" });
			}

			ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
			return View();
		}
	}
}
// endCHNhu