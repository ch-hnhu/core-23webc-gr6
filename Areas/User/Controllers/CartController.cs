using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Area("User")]
    public class CartController : Controller
    {
        // GET: CartController
        public ActionResult Index()
        {
            ViewData["BigTitle"] = "Shopping Cart";
            
            return View();
        }

    }
}
