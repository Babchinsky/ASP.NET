using _3_HW_Films_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _3_HW_Films_MVC.Controllers
{
    public class MovieController : Controller
    {
        MoviesContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MovieController(MoviesContext context, IWebHostEnvironment webHostEnvironment) 
        { 
            this._context = context; 
            this._webHostEnvironment = webHostEnvironment;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> movies = await _context.Movies.ToListAsync();
            return View(movies);
        }

        public IActionResult Create() { return View(); }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create(IFormFile uploadedFile, [Bind("Id,Title,Director,Genre,Date,PosterPath,Description")] Movie movie)
        {
            if (ModelState.IsValid && uploadedFile != null)
            {
                // Сохранение загруженного файла
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadedFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create)) await uploadedFile.CopyToAsync(fileStream);
                movie.PosterPath = "/Images/" + uniqueFileName; // Путь к сохранённому файлу

                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
        
        public async Task<IActionResult> Edit(int? id, IFormFile uploadedFile)
        {
            if (id == null) return NotFound();
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();
            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadedFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create)) await uploadedFile.CopyToAsync(fileStream);
                    movie.PosterPath = "/Images/" + uniqueFileName; // Обновляем путь к изображению
                }
                TryUpdateModelAsync(movie); // Попробуем обновить модель с данными из запроса
                _context.SaveChanges(); // Сохраняем все изменения в базе данных
                return RedirectToAction("Index"); // Редирект на страницу со списком фильмов
            }
            return View(movie); // Если модель не прошла валидацию, возвращаем ту же самую форму с ошибками
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var movie = await _context.Movies.FirstOrDefaultAsync(f => f.Id == id);
            if (movie == null) return NotFound();
            return View(movie);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var movie = await _context.Movies.FirstOrDefaultAsync(f => f.Id == id);
            if (movie == null) return NotFound();
            return View(movie);
        }
        
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null) _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
