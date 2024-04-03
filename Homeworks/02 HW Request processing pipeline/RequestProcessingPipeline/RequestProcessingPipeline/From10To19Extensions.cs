namespace RequestProcessingPipeline
{
    public static class From10To19Extensions
    {
        public static IApplicationBuilder UseFrom10To19(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From10To19Middleware>();
        }
    }
}
