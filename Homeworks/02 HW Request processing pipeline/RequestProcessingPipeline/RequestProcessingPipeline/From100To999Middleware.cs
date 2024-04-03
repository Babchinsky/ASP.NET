namespace RequestProcessingPipeline
{
    public class From100To999Middleware
    {
        private readonly RequestDelegate _next;

        public From100To999Middleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"]; // Получим число из контекста запроса
            try
            {
                string[] Hundreds = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);

                int hundreds = (number / 100) % 10;

                string? result = string.Empty;

                if (number < 100)
                {
                    await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                    result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware
                    await context.Response.WriteAsync("Your number is " + result);
                }
                else
                {
                    if (number % 100 == 0) 
                    {
                        await context.Response.WriteAsync("Your number is " + Hundreds[hundreds - 1] + " hundred");
                    }
                    else
                    {
                        await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                        result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware


                        await context.Response.WriteAsync("Your number is " + Hundreds[hundreds - 1] + " hundred " + result);
                      
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
