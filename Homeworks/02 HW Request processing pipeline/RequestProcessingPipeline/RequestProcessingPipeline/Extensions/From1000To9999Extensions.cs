using RequestProcessingPipeline.Middlewares;

namespace RequestProcessingPipeline.Extensions
{
    public static class From1000To9999Extensions
    {
        public static IApplicationBuilder UseFrom1000To9999(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From1000To9999Middleware>();
        }
    }
}
