using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Movies.Models;
using Movies.Services;



var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<MoviesContext>(options => options.UseSqlServer(connection));

builder.Services.AddRazorPages();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

app.Run();