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
			if (jsonUser != null)
			{
				var user = JsonSerializer.Deserialize<User>(jsonUser);
				if (user != null && user.role == "Admin")
				{
					// Đăng nhập thành công
					HttpContext.Session.SetString("AdminLoggedIn", jsonUser);
					return Redirect("/Admin/");
				}
				else
				{
					ViewBag.Error = "Tài khoản không có quyền truy cập.";
					return View();
				}
			}
			else
			{
				ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng.";
				return View();
			}
		}

	}
}
// endCHNhu