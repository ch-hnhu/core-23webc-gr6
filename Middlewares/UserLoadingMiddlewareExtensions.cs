//PNSon
namespace core_group_ex_01.Middlewares
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