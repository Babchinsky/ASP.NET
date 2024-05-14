using System.ComponentModel.DataAnnotations;
using Movies.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace Movies.Models
{
    public class Movie
    {
        //public int Id { get; set; }

        //[Required(ErrorMessage = "The Title field is required.")]
        //public string Title { get; set; }

        //[Required(ErrorMessage = "The Director field is required.")]
        //public string Director { get; set; }

        //[Required(ErrorMessage = "The Genre field is required.")]
        //public string Genre { get; set; }

        //[Required(ErrorMessage = "The Date field is required.")]
        //[DataType(DataType.Date)]
        //public DateTime Date { get; set; }

        //[Required(ErrorMessage = "The Description field is required.")]
        //public string Description { get; set; }

        //[Display(Name = "Poster")]
        //public string PosterPath { get; set; }


        public int Id { get; set; }

        [Required(ErrorMessage = "Поле \'Название должно быть заполнено")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 20 символов")]
        [Display(Name = "Название")]
        //[RestrictedMovies(["Собачье сердце", "Последнее искушение Христа", "Сало, или 120 дней Содома", "Каннибал Холокост", "Дау"], ErrorMessage = "Этот фильм запрещён")]
        //[Remote(action: "CheckTitle", controller: "Movie", ErrorMessage = "Этот фильм уже есть в Базе Данных")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Поле \'Режиссёр\' должно быть заполнено")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 20 символов")]
        [Display(Name = "Режиссёр")]
        public string? Director { get; set; }

        [Required(ErrorMessage = "Поле \'Жанр\' должно быть заполнено")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 20 символов")]
        [Display(Name = "Жанр")]
        public string? Genre { get; set; }

        [Required(ErrorMessage = "Поле \'Дата\' должно быть заполнено")]
        [Display(Name = "Дата")]
        [CustomDateRange("1800-01-01")]
        public DateTime Date { get; set; }

        //[Required(ErrorMessage = "Поле \'Фото\' должно быть заполнено")]
        [Display(Name = "Фото")]
        public string? PosterPath { get; set; }

        [Required(ErrorMessage = "Поле \'Описание\' должно быть заполнено")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Длина строки должна быть от 10 до 200 символов")]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

    }
}
