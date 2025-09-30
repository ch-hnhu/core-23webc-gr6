//Quoc Nam 
using System.Text;

namespace core_23webc_gr6.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath;

        public RequestLoggingMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _logFilePath = Path.Combine(env.ContentRootPath, "request.log");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string url = context.Request.Path;
            string ip = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string log = $"[{time}] IP: {ip}, URL: {url}{Environment.NewLine}";

            // Mở file với quyền chia sẻ để tránh bị lock
            using (var stream = new FileStream(_logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                await writer.WriteLineAsync(log);
            }

            await _next(context);
        }
    }

    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
