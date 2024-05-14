using Microsoft.EntityFrameworkCore;
using Movies.Models;

//var builder = WebApplication.CreateBuilder(args);
//string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<MoviesContext>(options => options.UseSqlServer(connection));
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Movie}/{action=Index}/{id?}");
//app.Run();


var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<MoviesContext>(options => options.UseSqlServer(connection));

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

app.Run();