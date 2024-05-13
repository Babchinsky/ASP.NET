using System.ComponentModel.DataAnnotations;
//using _3_HW_Films_MVC.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(255)] // Указываем максимальную длину для текстового поля
        public string Title { get; set; }
        [MaxLength(255)] // Указываем максимальную длину для текстового поля
        public string Director { get; set; }
        [MaxLength(255)] // Указываем максимальную длину для текстового поля
        public string Genre { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(255)] // Указываем максимальную длину для текстового поля
        public string PosterPath {  get; set; }
        [MaxLength(4000)] // Указываем максимальную длину для текстового поля
        public string Description { get; set; }

    }
}
