using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movies.Models;
using Movies.Services;

namespace Movies.Pages
{
    public class EditModel : PageModel
    {
        private readonly IMovieRepository _movieRepository;

        public EditModel(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        [Required(ErrorMessage = "The UploadedFile field is required.")]
        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _movieRepository.GetMovieByIdAsync(id.Value);

            if (Movie == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _movieRepository.UpdateMovieAsync(Movie, UploadedFile);

            return RedirectToPage("./Index");
        }
    }
}
