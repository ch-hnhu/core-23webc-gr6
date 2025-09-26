//PNSon
namespace core_23webc_gr6.Middlewares
{
    public static class UserLoadingMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserLoading(this IApplicationBuilder app)
        {
            return app.UseMiddleware<UserLoadingMiddleware>();
        }
    }
}
//endPNSon