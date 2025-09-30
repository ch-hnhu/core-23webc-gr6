//NTNguyen
using System.Text.Json;
using core_23webc_gr6.Models;
using core_23webc_gr6.Interfaces;

namespace core_23webc_gr6.Middlewares
{
    public class ProductMiddleware
    {
        private readonly RequestDelegate _next;

        public ProductMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Seeds", "db.json");
            if (File.Exists(filePath))
            {
                var json = await File.ReadAllTextAsync(filePath);
                var doc = JsonDocument.Parse(json);
                var products = JsonSerializer.Deserialize<List<Product>>(doc.RootElement.GetProperty("products"));

                // Đưa vào DI thông qua ProductRepository
                var repo = serviceProvider.GetRequiredService<IProductRepository>();
                repo.SetProducts(products!);
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
//endNTNguyen