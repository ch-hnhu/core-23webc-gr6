//PNSon
using System.Text.Json;
using core_23webc_gr6.Models;
using core_23webc_gr6.Services;

namespace core_23webc_gr6.Middlewares
{
    public class UserLoadingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _userFile = "Data/Seeds/db.json";

        public UserLoadingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            if (!userService.GetAllUsers().Any())
            {
                if (File.Exists(_userFile))
                {
                    //var json = await File.ReadAllTextAsync(_userFile);
                    //var users = JsonSerializer.Deserialize<List<User>>(doc.RootElement.GetProperty("users"));
                    //userService.SetUsers(users ?? new List<User>());
                    // //NTNguyen - Fix for new JSON structure with users and products
                    // var doc = JsonDocument.Parse(json);
                    // //endNTNguyen
                }
            }

            await _next(context);
        }
    }
}
//endPNSon