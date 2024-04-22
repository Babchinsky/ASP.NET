using _08_HW_11._04._2024_Music_Portal.Models;

namespace _08_HW_11._04._2024_Music_Portal.Repositories
{
    public interface ISongsRepository
    {
        Task<List<Song>> GetSongsListAsync();
        Task AddSongAndSaveAsync(Song song);
        Task<Song> FindSongByIdAsync(int songId);
        Task UpdateSongAsync(Song song);
        Task<Song> GetFisrtOrDefaultSongById(int songId);
        Task RemoveSongAsync(Song song);
        bool IsSongExistsById(int songId);
        IQueryable<Genre> Genres();
        IQueryable<Artist> Artists();
        
    }
}
