using System.ComponentModel.DataAnnotations;

namespace _07_HW_09._04._2024.Models
{
    // класс модели-представления (view-model)
    public class LoginModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }
    }
}
