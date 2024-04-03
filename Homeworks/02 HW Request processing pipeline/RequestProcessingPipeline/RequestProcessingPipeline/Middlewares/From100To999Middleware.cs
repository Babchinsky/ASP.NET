namespace RequestProcessingPipeline.Middlewares
{
    public class From100To999Middleware
    {
        private readonly RequestDelegate _next;

        public From100To999Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"]; // Получим число из контекста запроса
            try
            {
                string[] Hundreds = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);

                int last3Digits = number % 1000;
                int hundredsInLast3Digits = last3Digits / 100;

                string? result = string.Empty;

                if (last3Digits < 100)
                {
                    await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                    result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware
                    //await context.Response.WriteAsync("Your number is " + result);
                    context.Session.SetString("number", result);
                }
                else
                {
                    if (last3Digits % 100 == 0)
                    {
                        //await context.Response.WriteAsync("Your number is " + Hundreds[hundredsInLast3Digits - 1] + " hundred");
                        context.Session.SetString("number", Hundreds[hundredsInLast3Digits - 1] + " hundred");
                    }
                    else
                    {
                        await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                        result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware


                        //await context.Response.WriteAsync("Your number is " + Hundreds[hundredsInLast3Digits - 1] + " hundred " + result);
                        context.Session.SetString("number", Hundreds[hundredsInLast3Digits - 1] + " hundred " + result);
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
