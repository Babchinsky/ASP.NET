using Microsoft.EntityFrameworkCore;

namespace MusicPortal.Models
{
    public class MusicPortalContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        
        public MusicPortalContext(DbContextOptions<MusicPortalContext> options)
            : base(options)
        {
            if (Database.EnsureCreated())
            {
                Users.AddRange(
                new User { Login = "Алексей", Password = "Пароль1", AccessLevel = AccessLevel.UnverifiedUser },
                new User { Login = "Мария", Password = "Пароль2", AccessLevel = AccessLevel.VerifiedUser },
                new User { Login = "Елена", Password = "Пароль3", AccessLevel = AccessLevel.Admin },
                new User { Login = "Виктор", Password = "Пароль4", AccessLevel = AccessLevel.UnverifiedUser },
                new User { Login = "Дмитрий", Password = "Пароль5", AccessLevel = AccessLevel.VerifiedUser }

                //new User { Login = "Алексей", Password = "Пароль1" },
                //new User { Login = "Мария", Password = "Пароль2" },
                //new User { Login = "Елена", Password = "Пароль3" },
                //new User { Login = "Виктор", Password = "Пароль4" },
                //new User { Login = "Дмитрий", Password = "Пароль5" }
                );

                Artists.AddRange(
                    new Artist { Name = "The Beatles" },
                    new Artist { Name = "Michael Jackson" },
                    new Artist { Name = "Madonna" },
                    new Artist { Name = "Elton John" },
                    new Artist { Name = "Freddie Mercury" }
                );


                Genres.AddRange(
                    new Genre { Name = "Rock" },
                    new Genre { Name = "Pop" },
                    new Genre { Name = "Jazz" },
                    new Genre { Name = "Classical" },
                    new Genre { Name = "Hip Hop" }
                );


                SaveChanges();
            }
        }
    }
}
