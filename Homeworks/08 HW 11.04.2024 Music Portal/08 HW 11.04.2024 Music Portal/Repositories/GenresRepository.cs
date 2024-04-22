using _08_HW_11._04._2024_Music_Portal.Models;
using Microsoft.EntityFrameworkCore;

namespace _08_HW_11._04._2024_Music_Portal.Repositories
{
    public class GenresRepository: IGenresRepository
    {
        private readonly MusicPortalContext _context;

        public GenresRepository(MusicPortalContext context)
        {
            _context = context;
        }

        public async Task AddGenreAndSaveAsync(Genre genre)
        {
            _context.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task<Genre> FindGenreByIdAsync(int genreId)
        {
            return await _context.Genres.FindAsync(genreId);
        }

        public async Task<Genre> GetFisrtOrDefaultGenreById(int genreId)
        {
            return await _context.Genres.FirstOrDefaultAsync(m => m.Id == genreId);
        }

        public async Task<List<Genre>> GetGenresListAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public bool IsGenreExistsById(int genreId)
        {
            return _context.Genres.Any(e => e.Id == genreId);
        }

        public async Task RemoveGenreAsync(Genre genre)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGenreAsync(Genre genre)
        {
            _context.Update(genre);
            await _context.SaveChangesAsync();
        }
    }
}
