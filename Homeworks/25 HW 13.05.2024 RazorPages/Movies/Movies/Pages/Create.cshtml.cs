using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movies.Models;

namespace Movies.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Movies.Models.MoviesContext _context;
        IWebHostEnvironment _appEnvironment;

        public CreateModel(MoviesContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        [BindProperty]
        public IFormFile uploadedFile { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (uploadedFile != null)
            {
                // Ïóòü ê ïàïêå Files
                string path = "/Images/" + uploadedFile.FileName;


                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream); 
                }

                Movie.PosterPath = path;

                _context.Movies.Add(Movie);
                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}
