//NTNguyen
using System.Text.Json;
using core_23webc_gr6.Models;
using core_23webc_gr6.Interfaces;

namespace core_23webc_gr6.Middlewares
{
    public class ProductMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _productFile = "Data/Seeds/db.json";

        public ProductMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IProductRepository productRepository)
        {
            // Chỉ load nếu repo chưa có dữ liệu
            if (!productRepository.GetAllProducts().Any())
            {
                if (File.Exists(_productFile))
                {
                    var json = await File.ReadAllTextAsync(_productFile);
                    var doc = JsonDocument.Parse(json);
                    var products = JsonSerializer.Deserialize<List<Product>>(doc.RootElement.GetProperty("products"));

                    productRepository.SetProducts(products ?? new List<Product>());
                }
            }

            await _next(context);
        }
    }

    public static class ProductMiddlewareExtensions
    {
        public static IApplicationBuilder UseProductMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ProductMiddleware>();
        }
    }
}
//endNTNguyen cấu trúc nó giống nhau không 