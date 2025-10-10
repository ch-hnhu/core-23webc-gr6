using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using core_23webc_gr6.Models;

namespace core_23webc_gr6.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			// Đường dẫn tới file db.json trong Data/Seeds
			string filePath = Path.Combine(
				Directory.GetCurrentDirectory(),
				"Data", "Seeds", "db.json"
			);

			if (!System.IO.File.Exists(filePath))
			{
				throw new FileNotFoundException("Không tìm thấy file db.json tại: " + filePath);
			}

			string jsonData = System.IO.File.ReadAllText(filePath);

			using var doc = JsonDocument.Parse(jsonData);
			var productsElement = doc.RootElement.GetProperty("products");

			var products = JsonSerializer.Deserialize<List<Product>>(productsElement.GetRawText());

			// Lấy 6 sản phẩm đầu tiên
			var latestProducts = products?.Take(6).ToList() ?? new List<Product>();

			return View(latestProducts);
		}
	}
}
