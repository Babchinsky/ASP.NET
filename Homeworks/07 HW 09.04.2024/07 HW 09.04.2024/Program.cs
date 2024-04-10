using Microsoft.EntityFrameworkCore; 
using _07_HW_09._04._2024.Models;
using _07_HW_09._04._2024.Repositories;

var builder = WebApplication.CreateBuilder(args); // Создаем экземпляр объекта WebApplicationBuilder для конфигурации приложения


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();  

string? connection = builder.Configuration.GetConnectionString("DefaultConnection"); // Получаем строку подключения к базе данных из конфигурации приложения
builder.Services.AddDbContext<MessagesContext>(options => options.UseSqlServer(connection)); // Регистрируем контекст базы данных и указываем, что используем SQL Server как провайдер базы данных и строку подключения, которую мы получили ранее

builder.Services.AddControllersWithViews(); // Регистрируем поддержку контроллеров и представлений


builder.Services.AddScoped<IMessagesRepository, MessagesRepository>();

var app = builder.Build(); // Строим объект приложения на основе конфигурации

app.UseStaticFiles(); // Включаем обслуживание статических файлов (например, CSS, JavaScript) из папки wwwroot


app.UseSession(); // Включаем обслуживание сессий 



app.MapControllerRoute( // Настраиваем маршрутизацию для контроллеров и представлений
	name: "default", // Указываем имя маршрута
	pattern: "{controller=Main}/{action=Index}/{id?}"); // Определяем шаблон маршрута по умолчанию: контроллер по умолчанию - Home, действие по умолчанию - Index, параметр id является необязательным

app.Run(); // Запускаем приложение
