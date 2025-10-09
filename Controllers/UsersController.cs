using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Services;
using core_23webc_gr6.Models; //su dung AppConfig

namespace core_23webc_gr6.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: UsersController
        public IActionResult Index(int pageNumber = 1, int limit = 5)
        {
            
            var allUsers = _userService.GetAllUsers();

            var totalUsers = allUsers.Count;
            var totalPages = (int)Math.Ceiling((double)totalUsers / limit);

            // Đảm bảo pageNumber nằm trong phạm vi hợp lệ
            pageNumber = Math.Max(1, Math.Min(pageNumber, totalPages == 0 ? 1 : totalPages));

            var usersOnPage = allUsers.Skip((pageNumber - 1) * limit).Take(limit).ToList();

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(usersOnPage);
        }
    }
}
