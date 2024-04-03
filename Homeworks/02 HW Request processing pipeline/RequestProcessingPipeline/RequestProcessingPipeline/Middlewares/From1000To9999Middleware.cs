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
            string? token = context.Request.Query["number"]; // Получим число из контекста запроса

            try
            {
                string[] Thousands = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);

                int last4Digits = number % 10000;
                int thousandsInLast3Digits = last4Digits / 1000;

                string? result = string.Empty;

                if (last4Digits < 1000)
                {
                    await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                    result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware
                    await context.Response.WriteAsync("Your number is " + result);
                }

                else
                {
                    if (last4Digits % 100 == 0)
                    {
                        await context.Response.WriteAsync("Your number is " + Thousands[thousandsInLast3Digits - 1] + " thousand");
                    }
                    else
                    {
                        await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                        result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware


                        await context.Response.WriteAsync("Your number is " + Thousands[thousandsInLast3Digits - 1] + " thousand " + result);

                    }

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
