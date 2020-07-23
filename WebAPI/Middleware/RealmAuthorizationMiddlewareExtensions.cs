using Microsoft.AspNetCore.Builder;

namespace WebAPI.Middleware
{
    public static class RealmAuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseRealmAuthorization(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RealmAuthorizationMiddleware>();
        }
    }
}