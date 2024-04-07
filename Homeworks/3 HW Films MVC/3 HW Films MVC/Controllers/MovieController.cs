using _3_HW_Films_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _3_HW_Films_MVC.Controllers
{
    public class MovieController : Controller
    {
        MoviesContext db;
        public MovieController(MoviesContext db) 
        { 
            this.db = db; 
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> movies = await db.movies.ToListAsync();
            return View(movies);
        }
    }
}
