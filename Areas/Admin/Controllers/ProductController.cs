//PNSon
using core_23webc_gr6.Models; // Sử dụng model Product
using Microsoft.AspNetCore.Mvc; // Sử dụng Controller, IActionResult
using System.Text.Json; // Sử dụng JsonSerializer để đọc/ghi JSON

namespace core_23webc_gr6.Areas.Admin.Controllers
{
    [Area("Admin")] // Đánh dấu controller thuộc Area "Admin"
    public class ProductController : Controller
    {
        // Đường dẫn mặc định tới file db.json (có thể thay đổi nếu muốn dùng file khác)
        private const string DefaultJsonPath = "data/seeds/db.json";
        private const int PageSize = 9; // Số sản phẩm mỗi trang

        // Hiển thị danh sách sản phẩm với phân trang
        public IActionResult Index(int page = 1)
        {
            var products = GetProducts(); // Lấy danh sách sản phẩm từ file JSON
            int totalProducts = products.Count; // Tổng số sản phẩm
            int totalPages = (int)Math.Ceiling(totalProducts / (double)PageSize); // Tổng số trang

            // Lấy danh sách sản phẩm cho trang hiện tại
            var pagedProducts = products.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            ViewBag.Page = page; // Truyền số trang hiện tại sang View
            ViewBag.TotalPages = totalPages; // Truyền tổng số trang sang View

            return View(pagedProducts); // Trả về View với danh sách sản phẩm
        }

        // Hiển thị form thêm sản phẩm (GET)
        [HttpGet]
        public IActionResult AddProduct()
        {
            var product = new Product
            {
                MaSP = GenerateMaSP() // Sinh mã sản phẩm ngẫu nhiên
            };
            return View(product); // Trả về View với model Product mới
        }

        // Xử lý thêm sản phẩm (POST)
        [HttpPost]
        public IActionResult AddProduct(Product model, IFormFile? HinhAnhFile)
        {
            // Kiểm tra dữ liệu đầu vào
            if (!IsValidProduct(model, out string error))
            {
                ModelState.AddModelError(string.Empty, error); // Báo lỗi nếu dữ liệu không hợp lệ
                return View(model);
            }

            // Xử lý upload ảnh nếu có file ảnh
            if (HinhAnhFile != null && HinhAnhFile.Length > 0)
            {
                var ext = Path.GetExtension(HinhAnhFile.FileName).ToLower(); // Lấy phần mở rộng file
                var allowedExts = new[] { ".jpg", ".jpeg", ".png", ".gif" }; // Các định dạng cho phép
                if (!allowedExts.Contains(ext))
                {
                    ModelState.AddModelError("HinhAnh", "Chỉ chấp nhận file ảnh JPG, PNG, GIF.");
                    return View(model);
                }

                // Tạo tên file ngẫu nhiên để tránh trùng lặp
                var fileName = $"{Guid.NewGuid():N}{ext}";
                // Đường dẫn lưu file ảnh vào thư mục wwwroot/img
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    HinhAnhFile.CopyTo(stream); // Lưu file ảnh lên server
                }
                model.HinhAnh = fileName; // Lưu tên file ảnh vào model
            }

            var products = GetProducts(); // Lấy danh sách sản phẩm hiện tại
            // Đảm bảo mã sản phẩm không bị trùng
            while (products.Any(p => p.MaSP == model.MaSP))
            {
                model.MaSP = GenerateMaSP();
            }
            products.Add(model); // Thêm sản phẩm mới vào danh sách
            SaveProducts(products); // Ghi lại danh sách vào file JSON

            TempData["Message"] = "Thêm thành công!"; // Thông báo thành công
            return RedirectToAction("Index"); // Chuyển về trang danh sách sản phẩm
        }

        // Đọc danh sách sản phẩm từ file JSON
        private List<Product> GetProducts(string? jsonPath = null)
        {
            jsonPath ??= DefaultJsonPath; // Nếu không truyền đường dẫn thì dùng mặc định
            if (!System.IO.File.Exists(jsonPath))
                return new List<Product>(); // Nếu file chưa tồn tại thì trả về danh sách rỗng

            var json = System.IO.File.ReadAllText(jsonPath); // Đọc toàn bộ nội dung file JSON
            // Giả sử file có cấu trúc: { "products": [ ... ] }
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            var productsJson = data?["products"]?.ToString();
            var products = JsonSerializer.Deserialize<List<Product>>(productsJson ?? "[]");
            return products ?? new List<Product>();
        }

        // Ghi danh sách sản phẩm vào file JSON
        private void SaveProducts(List<Product> products, string? jsonPath = null)
        {
            jsonPath ??= DefaultJsonPath; // Nếu không truyền đường dẫn thì dùng mặc định
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
            data["products"] = products; // Gán danh sách sản phẩm vào key "products"
            var newJson = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }); // Ghi lại file với format đẹp
            System.IO.File.WriteAllText(jsonPath, newJson); // Ghi ra file
        }

        // Sinh mã sản phẩm ngẫu nhiên, ví dụ: SP123ABC
        private string GenerateMaSP()
        {
            return "SP" + new Random().Next(100, 999) + Guid.NewGuid().ToString("N")[..3].ToUpper();
        }

        // Kiểm tra dữ liệu đầu vào của sản phẩm
        private bool IsValidProduct(Product model, out string error)
        {
            error = "";
            // Tên sản phẩm: chỉ chữ, số, khoảng trắng
            if (string.IsNullOrWhiteSpace(model.TenSP) || !System.Text.RegularExpressions.Regex.IsMatch(model.TenSP, @"^[\p{L}\d\s]+$"))
            {
                error = "Tên sản phẩm chỉ được nhập chữ, số và khoảng trắng, không chứa ký tự đặc biệt.";
                return false;
            }
            // Đơn giá phải > 0, đơn giá khuyến mãi >= 0
            if (model.DonGia <= 0 || model.DonGiaKhuyenMai < 0)
            {
                error = "Đơn giá phải là số lớn hơn 0, đơn giá khuyến mãi phải là số không âm.";
                return false;
            }
            // Loại sản phẩm: chỉ chữ, số, khoảng trắng
            if (string.IsNullOrWhiteSpace(model.LoaiSP) || !System.Text.RegularExpressions.Regex.IsMatch(model.LoaiSP, @"^[\p{L}\d\s]+$"))
            {
                error = "Loại sản phẩm chỉ được nhập chữ, số và khoảng trắng, không chứa ký tự đặc biệt.";
                return false;
            }
            return true; // Dữ liệu hợp lệ
        }
    }
}
//endPNSon