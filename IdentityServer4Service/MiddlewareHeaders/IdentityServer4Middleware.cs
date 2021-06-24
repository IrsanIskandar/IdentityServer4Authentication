using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IdentityServer4Service.MiddlewareHeaders
{
    public class IdentityServer4Middleware
    {
        private readonly RequestDelegate _next;

        public IdentityServer4Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            return _next(httpContext);
        }
    }

    public static class IdentityServer4Extensions
    {
        public static IApplicationBuilder UseOBEMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IdentityServer4Middleware>();
        }
    }
}
