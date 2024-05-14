using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Movies.Models;
using Movies.Services;

namespace Movies.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IMovieRepository _movieRepository;

        public DetailsModel(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public Movie Movie { get; set; }

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
    }

}
