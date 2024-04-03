namespace RequestProcessingPipeline.Middlewares
{
    public class From1To9Middleware
    {
        private readonly RequestDelegate _next;

        public From1To9Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"]; // Получим число из контекста запроса
            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);

                string[] Ones = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                int units = number % 10; // единицы из любого числа
                context.Session.Clear();
                context.Session.SetString("number", Ones[units - 1]);
            }
            catch (Exception)
            {
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}
