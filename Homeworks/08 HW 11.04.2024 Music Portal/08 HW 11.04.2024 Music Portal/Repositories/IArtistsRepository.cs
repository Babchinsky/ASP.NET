using _08_HW_11._04._2024_Music_Portal.Models;

namespace _08_HW_11._04._2024_Music_Portal.Repositories
{
    public interface IArtistsRepository
    {
        Task<List<Artist>> GetArtistsListAsync();
        Task AddArtistAndSaveAsync(Artist artist);
        Task<Artist> FindArtistByIdAsync(int artistId);
        Task UpdateArtistAsync(Artist artist);
        Task<Artist> GetFisrtOrDefaultArtistByIdAsync(int artistId);
        Task RemoveArtistAsync(Artist artist);
        bool IsArtistExistsById(int artistId);
        Artist GetFirstOrDefaultArtistById(int artistId);
        
    }
}
