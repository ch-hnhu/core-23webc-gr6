using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
//VqNam 4/10/2025
namespace core_23webc_gr6.Areas.User.Controllers
{
    [Area("User")]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public ProductController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            var folderPath = Path.Combine(_env.WebRootPath, "assets", "user", "img");
            var files = Directory.GetFiles(folderPath)
                                 .Select(Path.GetFileName)
                                 .ToList();

            int pageSize = 12; // 4x3
            int maxPages = 5;  // chỉ cho 5 trang
            int totalPages = (int)System.Math.Ceiling((double)files.Count / pageSize);
            if (totalPages > maxPages) totalPages = maxPages;

            var pagedFiles = files.Skip((page - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToList();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;

            return View(pagedFiles);
        }
    }
}
//End VqNam 4/10/2025