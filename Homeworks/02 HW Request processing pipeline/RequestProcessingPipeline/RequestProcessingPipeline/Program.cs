using RequestProcessingPipeline.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Все сессии работают поверх объекта IDistributedCache, и 
// ASP.NET Core предоставляет встроенную реализацию IDistributedCache
builder.Services.AddDistributedMemoryCache();// добавляем IDistributedMemoryCache
builder.Services.AddSession();  // Добавляем сервисы сессии
var app = builder.Build();

app.UseSession();   // Добавляем middleware-компонент для работы с сессиями

// Добавляем middleware-компоненты в конвейер обработки запроса.
app.UseFrom20000To99999();
app.UseFrom10000To19999();
app.UseFrom1000To9999();
app.UseFrom100To999();
app.UseFrom20To99();
app.UseFrom10To19();
app.UseFrom1To9();

app.Run();

