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
			var url = context.Request.Path.ToString().ToLower();
			if (url != null && url.StartsWith("/admin") && !url.Contains("/auth/login"))
			{
				var isLoggedIn = context.Session.GetString("AdminLoggedIn");
				if (string.IsNullOrEmpty(isLoggedIn))
				{
					context.Response.Redirect("/Admin/Auth/Login");
					return;
				}
				else
				{
					await _next(context);
					return;
				}
			}
			await _next(context);
		}
	}
}
// endCHNhu