namespace RequestProcessingPipeline
{
    public class From20To99Middleware
    {
        private readonly RequestDelegate _next;

        public From20To99Middleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"]; // Получим число из контекста запроса
            try
            {
                string[] Tens = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                

                int last2Digits = number % 100;
                int tensInLast2Digits = last2Digits / 10;

                string? result = string.Empty;

                if (last2Digits <= 19) // если число меньше 20, то получаем от элементов результат. Либо с 1-9, либо с 10-19
                {
                    await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                    result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware

                    context.Session.SetString("number", result);
                }
                else
                {
                    if (last2Digits % 10 == 0)
                    {
                        //await context.Response.WriteAsync("Your number is " + Tens[tensInLast2Digits - 2]);
                        context.Session.SetString("number", Tens[tensInLast2Digits - 2]);
                    }
                    else
                    {
                        await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                        result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware
                        //await context.Response.WriteAsync("Your number is " + Tens[tensInLast2Digits - 2] + " " + result);
                        context.Session.SetString("number", Tens[tensInLast2Digits - 2] + " " + result);
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
