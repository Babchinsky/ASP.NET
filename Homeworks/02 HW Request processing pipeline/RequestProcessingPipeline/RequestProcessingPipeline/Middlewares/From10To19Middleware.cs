namespace RequestProcessingPipeline.Middlewares
{
    public class From10To19Middleware
    {
        private readonly RequestDelegate _next;

        public From10To19Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            try
            {
                string[] Numbers = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                int lastTwoDigits = number % 100; // последние 2 цифры из любого числа

                string? result = string.Empty;

                if (lastTwoDigits >= 10 && lastTwoDigits <= 19)
                {
                    context.Session.Clear();
                    context.Session.SetString("number", Numbers[lastTwoDigits - 10]);
                }
                else
                {
                    await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                }
            }
            catch (Exception)
            {
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}
