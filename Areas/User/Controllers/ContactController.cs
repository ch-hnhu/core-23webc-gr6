using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Area("User")]
    public class ContactController : Controller
    {
        // GET: ContactController
        public ActionResult Index()
        {
            ViewData["BigTitle"] = "Contact Us";
            return View();
        }

    }
}
