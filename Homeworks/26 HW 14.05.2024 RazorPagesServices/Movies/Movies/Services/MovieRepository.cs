using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public MovieRepository(MoviesContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public async Task AddMovieAsync(Movie movie, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // Сохраните файл в папке
                string path = "/Images/" + uploadedFile.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                movie.PosterPath = path;

                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateMovieAsync(Movie movie, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = "/Images/" + uploadedFile.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                movie.PosterPath = path;
            }

            _context.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }
    }

}
