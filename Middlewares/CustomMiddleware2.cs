using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Demos.API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleware2
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware2(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            using var buffer = new MemoryStream();
            var request = httpContext.Request;
            var response = httpContext.Response;

            var stream = response.Body;
            response.Body = buffer;

            await _next(httpContext);

            Debug.WriteLine($"Middleware 2");
            //await httpContext.Response.WriteAsync("Middleware 2");
            buffer.Position = 0;

            await buffer.CopyToAsync(stream);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddleware2Extensions
    {
        public static IApplicationBuilder UseCustomMiddleware2(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware2>();
        }
    }
}
