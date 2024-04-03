namespace RequestProcessingPipeline
{
    public static class From1To9Extensions
    {
        public static IApplicationBuilder UseFrom1To9(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From1To9Middleware>();
        }
    }
}
