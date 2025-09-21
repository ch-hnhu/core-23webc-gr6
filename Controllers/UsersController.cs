using Microsoft.AspNetCore.Mvc;
using core_group_ex_01.Services;

namespace core_group_ex_01.Controllers
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
