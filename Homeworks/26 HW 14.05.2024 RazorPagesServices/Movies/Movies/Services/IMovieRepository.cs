using Movies.Models;

namespace Movies.Services
{
    public interface IMovieRepository
    {
        Task AddMovieAsync(Movie movie, IFormFile uploadedFile);
        Task<Movie> GetMovieByIdAsync(int id);
        Task DeleteMovieAsync(int id);
        Task UpdateMovieAsync(Movie movie, IFormFile uploadedFile);
        Task<List<Movie>> GetAllMoviesAsync();
    }

}
