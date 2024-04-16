﻿using System.ComponentModel.DataAnnotations;

namespace _08_HW_11._04._2024_Music_Portal.Models
{
    public class Song
    {
        public int Id { get; set; }

        [Display(Name = "Песня")]
        public string Title { get; set; }

        [Display(Name = "Год выпуска")]
        public int Year { get; set; }  
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }
        public User User {  get; set; }
    }
}
