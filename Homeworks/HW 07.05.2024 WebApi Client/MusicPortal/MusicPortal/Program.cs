using MusicPortal.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.

builder.Services.AddDbContext<MusicPortalContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllers();

//builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseHttpsRedirection();
app.MapControllers();

app.Run();



