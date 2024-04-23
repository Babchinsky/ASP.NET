using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace _08_HW_11._04._2024_Music_Portal.Models
{
    public class MusicPortalContext : DbContext
    {
        public MusicPortalContext(DbContextOptions<MusicPortalContext> options) : base(options)
        {
            // Создаем базу данных, если не существует
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Добавление Artists
            modelBuilder.Entity<Artist>().HasData(
                new Artist { Id = 1, Name = "The Beatles" },
                new Artist { Id = 2, Name = "Elton John" },
                new Artist { Id = 3, Name = "Michael Jackson" },
                new Artist { Id = 4, Name = "Queen" },
                new Artist { Id = 5, Name = "David Bowie" },
                new Artist { Id = 6, Name = "Led Zeppelin" },
                new Artist { Id = 7, Name = "Pink Floyd" },
                new Artist { Id = 8, Name = "The Rolling Stones" },
                new Artist { Id = 9, Name = "Bob Dylan" },
                new Artist { Id = 10, Name = "Jimi Hendrix" }
            );

            // Добавление Genres
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Rock" },
                new Genre { Id = 2, Name = "Pop" },
                new Genre { Id = 3, Name = "Jazz" },
                new Genre { Id = 4, Name = "Blues" },
                new Genre { Id = 5, Name = "Reggae" },
                new Genre { Id = 6, Name = "Classical" },
                new Genre { Id = 7, Name = "Hip-Hop" },
                new Genre { Id = 8, Name = "Country" },
                new Genre { Id = 9, Name = "Metal" },
                new Genre { Id = 10, Name = "Soul" }
            );

            // Добавление Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Login = "admin", Password = "password123", Salt = "random_salt", AccessLevel = AccessLevel.Admin },
                new User { Id = 2, Login = "user1", Password = "password123", Salt = "random_salt", AccessLevel = AccessLevel.VerifiedUser },
                new User { Id = 3, Login = "user2", Password = "password123", Salt = "random_salt", AccessLevel = AccessLevel.UnverifiedUser },
                new User { Id = 4, Login = "user3", Password = "password123", Salt = "random_salt", AccessLevel = AccessLevel.VerifiedUser },
                new User { Id = 5, Login = "user4", Password = "password123", Salt = "random_salt", AccessLevel = AccessLevel.UnverifiedUser },
                new User { Id = 6, Login = "user5", Password = "password123", Salt = "random_salt", AccessLevel = AccessLevel.Admin },
                new User { Id = 7, Login = "user6", Password = "password123", Salt = "random_salt", AccessLevel = AccessLevel.VerifiedUser },
                new User { Id = 8, Login = "user7", Password = "password123", Salt = "random_salt", AccessLevel = AccessLevel.Admin },
                new User { Id = 9, Login = "user8", Password = "password123", Salt = "random_salt", AccessLevel = AccessLevel.UnverifiedUser },
                new User { Id = 10, Login = "user9", Password = "password123", Salt = "random_salt", AccessLevel = AccessLevel.VerifiedUser }
            );

            // Добавление Songs
            modelBuilder.Entity<Song>().HasData(
                new Song { Id = 1, Title = "Let It Be", Year = 1970, ArtistId = 1, GenreId = 1, UserId = 1, Path = "let_it_be.mp3" },
                new Song { Id = 2, Title = "Your Song", Year = 1970, ArtistId = 2, GenreId = 2, UserId = 1, Path = "your_song.mp3" },
                new Song { Id = 3, Title = "Thriller", Year = 1982, ArtistId = 3, GenreId = 2, UserId = 1, Path = "thriller.mp3" },
                new Song { Id = 4, Title = "Bohemian Rhapsody", Year = 1975, ArtistId = 4, GenreId = 1, UserId = 1, Path = "bohemian_rhapsody.mp3" },
                new Song { Id = 5, Title = "Space Oddity", Year = 1969, ArtistId = 5, GenreId = 1, UserId = 1, Path = "space_oddity.mp3" },
                new Song { Id = 6, Title = "Stairway to Heaven", Year = 1971, ArtistId = 6, GenreId = 1, UserId = 1, Path = "stairway_to_heaven.mp3" },
                new Song { Id = 7, Title = "Wish You Were Here", Year = 1975, ArtistId = 7, GenreId = 1, UserId = 1, Path = "wish_you_were_here.mp3" },
                new Song { Id = 8, Title = "Paint It Black", Year = 1966, ArtistId = 8, GenreId = 1, UserId = 1, Path = "paint_it_black.mp3" },
                new Song { Id = 9, Title = "Like a Rolling Stone", Year = 1965, ArtistId = 9, GenreId = 1, UserId = 1, Path = "like_a_rolling_stone.mp3" },
                new Song { Id = 10, Title = "Purple Haze", Year = 1967, ArtistId = 10, GenreId = 1, UserId = 1, Path = "purple_haze.mp3" }
            );
        }
    }
}
