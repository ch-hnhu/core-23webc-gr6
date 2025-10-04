// PNSon 4/10/2024 
using core_23webc_gr6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
//Thêm using để cấu hình encoder cho Unicode
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace core_23webc_gr6.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private const string DefaultJsonPath = "data/seeds/db.json";
        private const int PageSize = 9;

        public IActionResult Index(int page = 1)
        {
            var products = GetProducts();
            int totalProducts = products.Count;
            int totalPages = (int)Math.Ceiling(totalProducts / (double)PageSize);

            var pagedProducts = products.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;
            return View(pagedProducts);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var product = new Product
            {
                MaSP = GenerateMaSP()
            };
            return View(product);
        }

        [HttpPost]
        public IActionResult AddProduct(Product model, IFormFile? HinhAnhFile)
        {
            if (!ModelState.IsValid)   // Dùng DataAnnotations để validate
            {
                return View(model);
            }


            if (HinhAnhFile != null && HinhAnhFile.Length > 0)
            {
                var ext = Path.GetExtension(HinhAnhFile.FileName).ToLower();
                var allowedExts = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                if (!allowedExts.Contains(ext))
                {
                    ModelState.AddModelError("HinhAnh", "Chỉ chấp nhận file ảnh JPG, PNG, GIF.");
                    return View(model);
                }

                var fileName = $"{Guid.NewGuid():N}{ext}";
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Products", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    HinhAnhFile.CopyTo(stream);
                }

                //  Không lưu đường dẫn dài, chỉ lưu tên file
                model.HinhAnh = fileName;
            }

            var products = GetProducts();
            while (products.Any(p => p.MaSP == model.MaSP))
            {
                model.MaSP = GenerateMaSP();
            }

            products.Add(model);
            SaveProducts(products);

            TempData["Message"] = "Thêm thành công!";
            return RedirectToAction("Index");
        }

        private List<Product> GetProducts(string? jsonPath = null)
        {
            jsonPath ??= DefaultJsonPath;
            if (!System.IO.File.Exists(jsonPath))
                return new List<Product>();

            var json = System.IO.File.ReadAllText(jsonPath);
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            var productsJson = data?["products"]?.ToString();
            var products = JsonSerializer.Deserialize<List<Product>>(productsJson ?? "[]");

            //  Không tự động chỉnh sửa HinhAnh nữa
            return products ?? new List<Product>();
        }

        private void SaveProducts(List<Product> products, string? jsonPath = null)
        {
            jsonPath ??= DefaultJsonPath;
            Dictionary<string, object> data;
            if (System.IO.File.Exists(jsonPath))
            {
                var json = System.IO.File.ReadAllText(jsonPath);
                data = JsonSerializer.Deserialize<Dictionary<string, object>>(json) ?? new Dictionary<string, object>();
            }
            else
            {
                data = new Dictionary<string, object>();
            }
            data["products"] = products;
            // cho phép ghi Unicode không escape 4/10/2024
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            var newJson = JsonSerializer.Serialize(data, options);
            System.IO.File.WriteAllText(jsonPath, newJson);
        }

        public IActionResult Details(string id)
        {
            var products = GetProducts();
            var product = products.FirstOrDefault(p => p.MaSP == id);
            if (product == null) return NotFound();

            ViewData["Area"] = "Admin";

            return View("~/Views/Product/Details.cshtml", product);
        }

        private string GenerateMaSP()
        {
            return "SP" + new Random().Next(100, 999) + Guid.NewGuid().ToString("N")[..3].ToUpper();
        }
    }
}
// endPNSon
