namespace RequestProcessingPipeline.Middlewares
{
    public class From10000To19999Middleware
    {
        private readonly RequestDelegate _next;

        public From10000To19999Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"]; // Получим число из контекста запроса

            try
            {
                string[] TeensThousands = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);

                int last5Digits = number % 100000;
                int tensThousandsInLast5Digits = last5Digits / 10000; // 1 из числа 19123



                int teensThousandsInLast5Digits = last5Digits / 1000; // 19 из числа 19123

                string? result = string.Empty;

                if (last5Digits < 10000)
                {
                    await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                    result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware
                    //await context.Response.WriteAsync("Your number is " + result);
                    context.Session.SetString("number", result);
                }

                else
                {

                    if (teensThousandsInLast5Digits >= 10 && teensThousandsInLast5Digits <= 19)
                    {
                        if (last5Digits % 1000 == 0)
                        {
                            //await context.Response.WriteAsync("Your number is " + TeensThousands[teensThousandsInLast5Digits - 10] + " thousand");
                            context.Session.SetString("number", TeensThousands[teensThousandsInLast5Digits - 10] + " thousand");
                        }
                        else
                        {
                            await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                            result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware


                            //await context.Response.WriteAsync("Your number is " + TeensThousands[teensThousandsInLast5Digits - 10] + " thousand " + result);
                            context.Session.SetString("number", TeensThousands[teensThousandsInLast5Digits - 10] + " thousand " + result);
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
