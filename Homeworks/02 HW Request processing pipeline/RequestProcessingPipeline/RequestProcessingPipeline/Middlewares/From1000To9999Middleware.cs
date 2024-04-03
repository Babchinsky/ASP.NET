namespace RequestProcessingPipeline.Middlewares
{
    public class From1000To9999Middleware
    {
        private readonly RequestDelegate _next;

        public From1000To9999Middleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            //string? token = context.Request.Query["number"]; // Получим число из контекста запроса

            //try
            //{
            //    string[] Thousands = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            //    int number = Convert.ToInt32(token);
            //    number = Math.Abs(number);

            //    //int hundreds = number / 100 % 10;

            //    //string? result = string.Empty;


            //}
            //catch (Exception)
            //{
            //    // Выдаем окончательный ответ клиенту
            //    await context.Response.WriteAsync("Incorrect parameter");
            //}
        }
    }
}
