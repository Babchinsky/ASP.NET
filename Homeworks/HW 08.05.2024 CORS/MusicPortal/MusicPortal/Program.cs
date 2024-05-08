using MusicPortal.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(); // добавляем сервисы CORS

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.

builder.Services.AddDbContext<MusicPortalContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();
//app.UseStaticFiles();

app.UseCors(builder => builder.WithOrigins("https://localhost:7112")
                    .AllowAnyHeader().AllowAnyMethod());

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();

app.Run();



