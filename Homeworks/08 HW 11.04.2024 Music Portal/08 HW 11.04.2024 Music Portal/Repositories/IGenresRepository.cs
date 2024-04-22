using _08_HW_11._04._2024_Music_Portal.Models;

namespace _08_HW_11._04._2024_Music_Portal.Repositories
{
    public interface IGenresRepository
    {
        Task<List<Genre>> GetGenresListAsync();
        Task AddGenreAndSaveAsync(Genre genre);
        Task<Genre> FindGenreByIdAsync(int genreId);
        Task UpdateGenreAsync(Genre genre);
        Task<Genre> GetFisrtOrDefaultGenreById(int genreId);
        Task RemoveGenreAsync(Genre genre);
        bool IsGenreExistsById(int genreId);
        
    }
}
