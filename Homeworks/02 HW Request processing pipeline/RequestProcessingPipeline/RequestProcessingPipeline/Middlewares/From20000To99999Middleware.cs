namespace RequestProcessingPipeline.Middlewares
{
    public class From20000To99999Middleware
    {
        private readonly RequestDelegate _next;

        public From20000To99999Middleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"]; // Получим число из контекста запроса

            try
            {
                string[] TensThousands = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);


                int last5Digits = number % 100000;
                int tensThousandsInLast5Digits = last5Digits / 10000; // 1 из числа 19123

                string? result = string.Empty;

                if (last5Digits < 20000)
                {
                    await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                    result = context.Session.GetString("number"); // получим число от компонента From10000To19999
                    await context.Response.WriteAsync("Your number is " + result);
                }
                //else if (number == 10000){
                //    await context.Response.WriteAsync("Your number is one hundred thousand");
                //}
                //else if (number > 100000)
                //{
                //    await context.Response.WriteAsync("Your number is greater than one hundred thousand");
                //}
                //else
                //{
                //    await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                //    result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware


                //    //context.Session.SetString("number", TensThousands[tensThousandsInLast5Digits - 1] + " hundred " + result);
                //    await context.Response.WriteAsync("Your number is " + TensThousands[tensThousandsInLast5Digits - 1] + " hundred " + result);
                //}

            }
            catch (Exception)
            {
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}
