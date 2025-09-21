using Microsoft.AspNetCore.Mvc;
using core_group_ex_01.Services;
//Kieu
using core_group_ex_01.Models; //su dung AppConfig

namespace core_group_ex_01.Controllers
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
        public IActionResult Index()
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

            var users = _userService.GetAllUsers();
            return View(users);
        }
    }
}
