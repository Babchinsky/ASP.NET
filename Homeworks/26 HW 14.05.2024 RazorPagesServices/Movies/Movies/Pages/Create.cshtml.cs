using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movies.Models;
using Movies.Services;

namespace Movies.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IMovieRepository _movieRepository;

        public CreateModel(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
       
            await _movieRepository.AddMovieAsync(Movie, UploadedFile);

            return RedirectToPage("./Index");
        }
    }
}
