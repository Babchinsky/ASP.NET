using System.ComponentModel.DataAnnotations;

namespace _08_HW_11._04._2024_Music_Portal.Models
{
    public class Song
    {
        public int Id { get; set; }

        [Display(Name = "Песня")]
        [Required(ErrorMessage = "Поле 'Песня' обязательно для заполнения")]
        public string Title { get; set; }

        [Display(Name = "Год выпуска")]
        [Required(ErrorMessage = "Поле 'Год выпуска' обязательно для заполнения")]
        public int Year { get; set; }

        [Display(Name = "Идентификатор исполнителя")]
        public int ArtistId { get; set; }

        [Display(Name = "Идентификатор жанра")]
        public int GenreId { get; set; }

        [Display(Name = "Идентификатор пользователя")]
        public int UserId { get; set; }

        [Display(Name = "Файл")]
        public string? Path { get; set; }


        public Artist Artist { get; set; }
        public Genre Genre { get; set; }
        public User User { get; set; }
    }
}
