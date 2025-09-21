using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class UsersController : Controller
    {
        // GET: UsersController
        public ActionResult Index()
        {
            return View();
        }
    }
}
