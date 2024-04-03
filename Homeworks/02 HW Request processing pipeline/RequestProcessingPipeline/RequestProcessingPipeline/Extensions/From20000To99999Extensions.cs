using RequestProcessingPipeline.Middlewares;

namespace RequestProcessingPipeline.Extensions
{
    public static class From20000To99999Extensions
    {
        public static IApplicationBuilder UseFrom20000To99999(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From20000To99999Middleware>();
        }
    }
}
