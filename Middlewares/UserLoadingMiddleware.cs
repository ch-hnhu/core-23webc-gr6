//PNSon
using System.Text.Json;
using core_group_ex_01.Models;
using core_group_ex_01.Services;

namespace core_group_ex_01.Middlewares
{
    public class UserLoadingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _userFile = "Data/users.json";

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
                    var json = await File.ReadAllTextAsync(_userFile);
                    var users = JsonSerializer.Deserialize<List<User>>(json);
                    userService.SetUsers(users ?? new List<User>());
                }
            }

            await _next(context);
        }
    }
}
//endPNSon