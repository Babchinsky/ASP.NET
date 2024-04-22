using _08_HW_11._04._2024_Music_Portal.Models;
using Microsoft.EntityFrameworkCore;

namespace _08_HW_11._04._2024_Music_Portal.Repositories
{
    public class SongsRepository: ISongsRepository
    {
        private readonly MusicPortalContext _context;

        public SongsRepository(MusicPortalContext context)
        {
            _context = context;
        }

        public async Task AddSongAndSaveAsync(Song song)
        {
            _context.Add(song);
            await _context.SaveChangesAsync();
        }

        public async Task<Song> FindSongByIdAsync(int songId)
        {
            return await _context.Songs.FindAsync(songId);
        }

        public IQueryable<Artist> Artists()
        {
            return _context.Artists;
        }

        public async Task<Song> GetFisrtOrDefaultSongById(int songId)
        {
            return await _context.Songs.FirstOrDefaultAsync(m => m.Id == songId);
        }

        public IQueryable<Genre> Genres()
        {
            return _context.Genres;
        }

        public async Task<List<Song>> GetSongsListAsync()
        {
            return await _context.Songs
                                    .Include(s => s.Artist)
                                    .Include(s => s.Genre)
                                    .ToListAsync();
        }

        public bool IsSongExistsById(int songId)
        {
            return _context.Songs.Any(e => e.Id == songId);
        }

        public async Task RemoveSongAsync(Song song)
        {
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSongAsync(Song song)
        {
            _context.Update(song);
            await _context.SaveChangesAsync();
        }
    }
}
