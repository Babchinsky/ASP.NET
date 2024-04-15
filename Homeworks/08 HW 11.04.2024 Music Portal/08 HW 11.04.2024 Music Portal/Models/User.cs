using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _08_HW_11._04._2024_Music_Portal.Models
{
    public enum AccessLevel
    {
        [Display(Name = "Неподтвержденный пользователь")]
        UnverifiedUser,
        [Display(Name = "Подтвержденный пользователь")]
        VerifiedUser,
        [Display(Name = "Администратор")]
        Admin
    }

    public class User
    {
        public User()
        {
            this.Songs = new HashSet<Song>();
        }

        public int Id { get; set; }

        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Соль")]
        public string Salt { get; set; }

        [Display(Name = "Уровень доступа")]
        public AccessLevel AccessLevel { get; set; }

        [Display(Name = "Песни")]
        public ICollection<Song> Songs { get; set; }
    }
}
