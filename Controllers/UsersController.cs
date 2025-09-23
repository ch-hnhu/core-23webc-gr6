using Microsoft.AspNetCore.Mvc;
using core_group_ex_01.Services;
using core_group_ex_01.Models;

namespace MyApp.Namespace
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: UsersController
        public ActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }
    }
}
