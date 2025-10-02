// Areas/Admin/Controllers/ProductController.cs
// PNSon
using core_23webc_gr6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
            if (!IsValidProduct(model, out string error))
            {
                ModelState.AddModelError(string.Empty, error);
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

                // luôn lưu với đường dẫn web, để view load được
                model.HinhAnh = "/img/Products/" + fileName;
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

            // Chuẩn hóa đường dẫn ảnh: nếu chưa có "/img" thì thêm vào "/img/"
            foreach (var p in products ?? new List<Product>())
            {
                if (!string.IsNullOrEmpty(p.HinhAnh) && !p.HinhAnh.StartsWith("/img"))
                {
                    // cũ có thể chỉ lưu tên file => mặc định để ở /img/
                    p.HinhAnh = "/img/" + p.HinhAnh;
                }
            }

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
            var newJson = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(jsonPath, newJson);
        }
        public IActionResult Details(string id)
        {
            var products = GetProducts();
            var product = products.FirstOrDefault(p => p.MaSP == id);
            if (product == null) return NotFound();

            // Gửi thông tin area xuống view
            ViewData["Area"] = "Admin";

            return View("~/Views/Product/Details.cshtml", product);
        }
        private string GenerateMaSP()
        {
            return "SP" + new Random().Next(100, 999) + Guid.NewGuid().ToString("N")[..3].ToUpper();
        }

        private bool IsValidProduct(Product model, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(model.TenSP) || !System.Text.RegularExpressions.Regex.IsMatch(model.TenSP, @"^[\p{L}\d\s]+$"))
            {
                error = "Tên sản phẩm chỉ được nhập chữ, số và khoảng trắng, không chứa ký tự đặc biệt.";
                return false;
            }
            if (model.DonGia <= 0 || model.DonGiaKhuyenMai < 0)
            {
                error = "Đơn giá phải là số lớn hơn 0, đơn giá khuyến mãi phải là số không âm.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(model.LoaiSP) || !System.Text.RegularExpressions.Regex.IsMatch(model.LoaiSP, @"^[\p{L}\d\s]+$"))
            {
                error = "Loại sản phẩm chỉ được nhập chữ, số và khoảng trắng, không chứa ký tự đặc biệt.";
                return false;
            }
            return true;
        }
    }
}
// endPNSon
