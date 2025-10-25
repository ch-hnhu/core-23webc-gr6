using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Models;
using core_23webc_gr6.Helper;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace core_23webc_gr6.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		// CHNhu - 12/10/2025 - Chỉnh lại lấy dssp từ db dùng DatabaseHelper
		private readonly DatabaseHelper _db;
		private readonly IWebHostEnvironment _env;
		public ProductController(DatabaseHelper db,IWebHostEnvironment env )
		{
			_db = db;
			_env = env;
		}
		public IActionResult Index(int page = 1, int pageSize = 5)
		{
			List<Products> products = new();

			string query = "select * from Products";

			SqlConnection conn = _db.GetConnection();
			conn.Open();
			SqlDataReader dr = _db.ExcuteQuery(query, conn);
			while (dr.Read())
			{
				products.Add(new Products
				{
					ProductID = Convert.ToInt32(dr["ProductID"]),
					ProductName = dr["ProductName"].ToString() ?? string.Empty,
					CategoryID = dr["CategoryID"] as int?,
					Price = Convert.ToDecimal(dr["Price"]),
					DiscountPercentage = Convert.ToInt32(dr["DiscountPercentage"]),
					Stock = Convert.ToInt32(dr["Stock"]),
					Image = dr["Image"].ToString(),
					Description = dr["Description"].ToString(),
					Status = Convert.ToByte(dr["Status"]),
					CreatedAt = Convert.ToDateTime(dr["CreatedAt"]),
					UpdatedAt = Convert.ToDateTime(dr["UpdatedAt"])
				});
			}
			dr.Close();
			conn.Close();

			int totalProducts = products.Count();

			int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
			if (page < 1) page = 1;
			if (page > totalPages) page = totalPages;

			var pagedProducts = products
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			ViewBag.Page = page;
			ViewBag.TotalPages = totalPages;

			return View(pagedProducts);
		}
		[HttpGet]
		public IActionResult AddProduct()
		{
			return View(new Products());
		}

		[HttpPost]
		public async Task<ActionResult> AddProduct(Products model)
        {
			try
			{
				//lấy tất cả file từ request.Form.Files
				var files = Request.Form.Files;

				//Tạo thư mục lưu hình ảnh
				string uploadPath = Path.Combine(_env.WebRootPath, "img", "Products");
				if (!Directory.Exists(uploadPath))
					Directory.CreateDirectory(uploadPath);
				List<string> fileNames = new List<string>();

				//Duyệt qua tất cả file được upload
				foreach (var file in files)
				{
					if (file != null && file.Length > 0)
					{
						//Tạo tên file unique
						string fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
						string filePath = Path.Combine(uploadPath, fileName);
						//Luu file
						using (var stream = new FileStream(filePath, FileMode.Create))
						{
							await file.CopyToAsync(stream);
						}
						fileNames.Add(fileName);
					}
				}
				//lưu danh sấch tên ảnh vào model (cách nhau bởi dấu phẩy)
				model.Image = string.Join(",", fileNames);

				//đường dẫn file json
				string jsonPath = Path.Combine(_env.ContentRootPath, "Data", "product.json");
				//Tạo thư mục Data 
				string dataFolder = Path.Combine(_env.ContentRootPath, "Data");
				if (!Directory.Exists(dataFolder))
					Directory.CreateDirectory(dataFolder);
				//đọc file json
				List<Products> products = new();
				if (System.IO.File.Exists(jsonPath))
				{
					string json = await System.IO.File.ReadAllTextAsync(jsonPath);
					products = JsonSerializer.Deserialize<List<Products>>(json) ?? new List<Products>();
				}
				//Thêm sản phẩm mới
				products.Add(model);
				//Ghi lại Json
				var options = new JsonSerializerOptions
				{
					WriteIndented = true,
					Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
				};
				string updated = JsonSerializer.Serialize(products, options);
				await System.IO.File.WriteAllTextAsync(jsonPath, updated);

				TempData["Success"] = "Thêm thành công";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
            {
				TempData["Error"] = "Lỗi" + ex.Message;
				return View(model);
            }

		}
	}
}
