using MusicPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AjaxMvcApplication.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly MusicPortalContext _context;

        public ArtistsController(MusicPortalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetArtists()
        {
            if (_context.Artists == null)
                return Problem("Список исполнителей пустой!");
            List<Artist> list = await _context.Artists.ToListAsync();
            string response = JsonConvert.SerializeObject(list);
            return Json(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetDetailsById(int id)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }
            string response = JsonConvert.SerializeObject(artist);
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> InsertArtist(Artist artist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artist);
                await _context.SaveChangesAsync();
                string response = "Исполнитель успешно добавлен!";
                return Json(response);
            }
            return Problem("Проблемы при добавлении исполнителя!");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateArtist(Artist artist)
        {
            if (ModelState.IsValid)
            {
                _context.Update(artist);
                await _context.SaveChangesAsync();
                string response = "Исполнитель успешно изменен!";
                return Json(response);
            }
            return Problem("Проблемы при редактировании исполнителя!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            if (_context.Artists == null)
            {
                return Problem("Список исполнителей пустой!");
            }
            var artist = await _context.Artists.FindAsync(id);
            if (artist != null)
            {
                _context.Artists.Remove(artist);
            }
            else
            {
                return Problem("Нет такого исполнителя!");
            }
            await _context.SaveChangesAsync();
            string response = "Исполнитель успешно удален!";
            return Json(response);
        }
    }
}