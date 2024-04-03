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
                int thousandsInLast4Digits = last4Digits / 1000;
                

                string? result = string.Empty;



                int hundredsInLast4Digits = (last4Digits / 100) % 10; // 9 из 1900 


                int last5Digits = number % 100000;
                int teensInLast5Digits = last5Digits / 1000;


                if (last4Digits < 1000)
                {
                    await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                    result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware
                    //await context.Response.WriteAsync("Your number is " + result);
                    context.Session.SetString("number", result);
                }
                else
                {
                    if (teensInLast5Digits >= 10 && teensInLast5Digits<= 19) 
                    {
                        await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                    }
                    else
                    {
                        if (last4Digits % 100 == 0 && hundredsInLast4Digits == 0)
                        {
                            //await context.Response.WriteAsync("Your number is " + Thousands[thousandsInLast4Digits - 1] + " thousand");
                            context.Session.SetString("number", Thousands[thousandsInLast4Digits - 1] + " thousand");
                        }
                        else
                        {
                            await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                            result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware

                            //await context.Response.WriteAsync("Your number is " + Thousands[thousandsInLast4Digits - 1] + " thousand " + result);
                            context.Session.SetString("number", Thousands[thousandsInLast4Digits - 1] + " thousand " + result);

                        }
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
