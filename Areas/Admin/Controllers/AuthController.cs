// CHNhu - 20/10/2025 - Tạo AuthController cho Admin
using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Helper;
using core_23webc_gr6.Models;
using System.Text.Json;

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
			string? jsonUser = Models.User.GetUser(username, password, _dbHelper);

			if (!string.IsNullOrEmpty(jsonUser))
			{
				var user = JsonSerializer.Deserialize<User>(jsonUser);

				if (user != null && user.role == "Admin")
				{
					HttpContext.Session.SetString("AdminLoggedIn", jsonUser);
					return Redirect("/Admin/Home/Index");
				}
				else
				{
					ViewBag.Error = "Bạn không có quyền truy cập trang quản trị.";
					return View();
				}
			}

			ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
			return View();
		}
	}
}
// endCHNhu