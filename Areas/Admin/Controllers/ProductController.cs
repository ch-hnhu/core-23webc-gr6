using Microsoft.AspNetCore.Mvc;

namespace core_23webc_gr6.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

    }
}
