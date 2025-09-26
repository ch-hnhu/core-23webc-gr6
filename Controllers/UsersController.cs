using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Services;
using core_23webc_gr6.Models; //su dung AppConfig

namespace core_23webc_gr6.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        //Kieu
        private readonly AppConfig _config;

        public UsersController(IUserService userService, AppConfig config)
        {
            _userService = userService;
            //Kieu
            _config = config;
        }

        // GET: UsersController
        public IActionResult Index(int pageNumber = 1, int limit = 5)
        {
            //Kieu
            //Kiem tra IP co trong danh sach IP cho phep hay khong
            //HttpContext.Connection.RemoteIpAddress se tra ve IP cua nguoi dung dang truy cap
            // ?. la neu khong null thi goi tiep, null thi tra ve null
            var userIP = HttpContext.Connection.RemoteIpAddress?.ToString();
            // ?? la neu userIP null thi gan gia tri "" cho userIP
            if (_config.BannerIPs.Contains(userIP ?? ""))
            {
                ViewBag.Message = "IP của bạn bị cấm.";
                return View("Error");
            }
            
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
