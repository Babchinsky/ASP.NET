using MusicPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AjaxMvcApplication.Controllers
{
    public class GenresController : Controller
    {
        private readonly MusicPortalContext _context;

        public GenresController(MusicPortalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            if (_context.Genres == null)
                return Problem("Список жанров пустой!");
            List<Genre> list = await _context.Genres.ToListAsync();
            string response = JsonConvert.SerializeObject(list);
            return Json(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetDetailsById(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            string response = JsonConvert.SerializeObject(genre);
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> InsertGenre(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genre);
                await _context.SaveChangesAsync();
                string response = "Жанр успешно добавлен!";
                return Json(response);
            }
            return Problem("Проблемы при добавлении жанра!");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateGenre(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Update(genre);
                await _context.SaveChangesAsync();
                string response = "Жанр успешно изменен!";
                return Json(response);
            }
            return Problem("Проблемы при редактировании жанра!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            if (_context.Genres == null)
            {
                return Problem("Список жанров пустой!");
            }
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
            }
            else
            {
                return Problem("Нет такого жанра!");
            }
            await _context.SaveChangesAsync();
            string response = "Жанр успешно удален!";
            return Json(response);
        }
    }
}