using _08_HW_11._04._2024_Music_Portal.Models;
using Microsoft.EntityFrameworkCore;

namespace _08_HW_11._04._2024_Music_Portal.Repositories
{
    public class MusicDataRepository: IMusicDataRepository
    {
        private readonly MusicPortalContext _context;

        public MusicDataRepository(MusicPortalContext context)
        {
            _context = context;
        }

        public async Task AddArtistAndSaveAsync(Artist artist)
        {
            _context.Add(artist);
            await _context.SaveChangesAsync();
        }

        public async Task<Artist> FindArtistByIdAsync(int artistId)
        {
            return await _context.Artists.FindAsync(artistId);
        }

        public async Task<List<Artist>> GetArtistsListAsync()
        {
            return await _context.Artists.ToListAsync();
        }

        public async Task<Artist> GetFisrtOrDefaultArtistById(int artistId)
        {
            return await _context.Artists.FirstOrDefaultAsync(m => m.Id == artistId);
        }

        public bool IsArtistExistsById(int artistId)
        {
            return _context.Artists.Any(e => e.Id == artistId);
        }


        public async Task RemoveArtistAsync(Artist artist)
        {
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateArtistAsync(Artist artist)
        {
            _context.Update(artist);
            await _context.SaveChangesAsync();
        }
    }
}
