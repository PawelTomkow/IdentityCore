using Microsoft.AspNetCore.Builder;

namespace Identity.Extensions
{
    public static class AppBuilderExtension
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ExceptionHandlerMiddleware));
    }
}