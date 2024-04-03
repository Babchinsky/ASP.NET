using RequestProcessingPipeline.Middlewares;

namespace RequestProcessingPipeline.Extensions
{
    public static class From10000To19999Extensions
    {
        public static IApplicationBuilder UseFrom10000To19999(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From10000To19999Middleware>();
        }
    }
}
