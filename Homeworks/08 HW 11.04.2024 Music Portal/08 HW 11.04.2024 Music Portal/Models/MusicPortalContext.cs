using Microsoft.EntityFrameworkCore;

namespace _08_HW_11._04._2024_Music_Portal.Models
{
    public class MusicPortalContext: DbContext
    {
        public MusicPortalContext(DbContextOptions<MusicPortalContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
    }

}
