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
    public class IndexModel : PageModel
    {
        private readonly IMovieRepository _movieRepository;

        public IndexModel(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public IList<Movie> Movies { get; set; }

        public async Task OnGetAsync()
        {
            Movies = await _movieRepository.GetAllMoviesAsync();
        }
    }

}
