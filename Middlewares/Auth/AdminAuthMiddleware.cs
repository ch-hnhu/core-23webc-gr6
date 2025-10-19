// CHNhu - 12/10/2025
namespace core_23webc_gr6.Middlewares
{
	public class AdminAuthMiddleware
	{
		private readonly RequestDelegate _next;

		public AdminAuthMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var path = context.Request.Path.Value?.ToLower();

			// Nếu truy cập Admin (trừ login) và chưa có session thì redirect
			if (path != null && path.StartsWith("/admin") && !path.Contains("/auth/login"))
			{
				var isLoggedIn = context.Session.GetString("AdminLoggedIn");
				if (!string.IsNullOrEmpty(isLoggedIn))
				{
					await _next(context);
					return;
				}
				else
				{
					context.Response.Redirect("/Admin/Auth/Login");
					return;
				}
			}

			await _next(context);
		}
	}

	public static class AdminAuthMiddlewareExtensions
	{
		public static IApplicationBuilder UseAdminAuth(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<AdminAuthMiddleware>();
		}
	}
}
// endCHNhu