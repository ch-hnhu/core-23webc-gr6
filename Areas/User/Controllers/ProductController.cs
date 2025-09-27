using Microsoft.AspNetCore.Mvc;

namespace core_23webc_gr6.Areas.User.Controllers
{
    [Area("User")]
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id = 1)
        {
            return View();
        }
    }
}
