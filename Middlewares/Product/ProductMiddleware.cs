//NTNguyen
using System.Text.Json;
using core_23webc_gr6.Models;
using core_23webc_gr6.Helper;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace core_23webc_gr6.Middlewares
{
	public class ProductMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly string _productFile = "Data/Seeds/db.json";
		//LTMKieu 18/10/2025
		private readonly DatabaseHelper _db;

		public ProductMiddleware(RequestDelegate next, DatabaseHelper db)
		{
			_next = next;
			_db = db;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			// Chỉ load nếu DB chưa có dữ liệu
			using (var conn = _db.GetConnection())
			{
				await conn.OpenAsync();

				// Kiểm tra count trong bảng Products
				var countCmd = new SqlCommand("SELECT COUNT(1) FROM Products", conn);
				var result = await countCmd.ExecuteScalarAsync();
				var count = (result is int) ? (int)result : Convert.ToInt32(result);

				if (count == 0 && File.Exists(_productFile))
				{
					var json = await File.ReadAllTextAsync(_productFile);
					var doc = JsonDocument.Parse(json);
					var products = JsonSerializer.Deserialize<List<Products>>(doc.RootElement.GetProperty("products").GetRawText());

					if (products != null && products.Any())
					{
						// Lấy các property của model (bỏ qua Id nếu có)
						var props = typeof(Products).GetProperties(BindingFlags.Public | BindingFlags.Instance)
													.Where(p => !string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase))
													.ToArray();

						var columns = string.Join(", ", props.Select(p => $"[{p.Name}]"));
						var parameters = string.Join(", ", props.Select(p => $"@{p.Name}"));

						foreach (var prod in products)
						{
							using var insertCmd = new SqlCommand($"INSERT INTO Products ({columns}) VALUES ({parameters})", conn);

							foreach (var p in props)
							{
								var val = p.GetValue(prod) ?? DBNull.Value;
								insertCmd.Parameters.AddWithValue("@" + p.Name, val);
							}

							await insertCmd.ExecuteNonQueryAsync();
						}
					}
				}

			}

			await _next(context);
		}
		//endLTMKieu
	}

	public static class ProductMiddlewareExtensions
	{
		public static IApplicationBuilder UseProductMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ProductMiddleware>();
		}
	}
}
//endNTNguyen