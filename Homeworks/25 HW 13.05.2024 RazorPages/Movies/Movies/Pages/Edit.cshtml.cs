using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Pages
{
    public class EditModel : PageModel
    {
        private readonly Movies.Models.MoviesContext _context;
        IWebHostEnvironment _appEnvironment;

        public EditModel(Movies.Models.MoviesContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie =  await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            Movie = movie;
            return Page();
        }

        [BindProperty]
        public IFormFile uploadedFile { get; set; } = default!;
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (uploadedFile != null)
            {
                string path = "/Images/" + uploadedFile.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                // Получаем Movie из базы данных по его Id
                var movieFromDb = await _context.Movies.FindAsync(Movie.Id);

                // Проверяем, найден ли фильм
                if (movieFromDb != null)
                {
                    // Обновляем путь к постеру
                    movieFromDb.PosterPath = path;

                    // Сохраняем изменения в базе данных
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Если фильм не найден, можно выполнить какие-то действия по обработке ошибки
                    return NotFound();
                }
            }

            return RedirectToPage("./Index");
        }


        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
