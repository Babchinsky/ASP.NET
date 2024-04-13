using System.ComponentModel.DataAnnotations;

namespace _08_HW_11._04._2024_Music_Portal.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Поле '{0}' обязательно для заполнения")]
        [Display(Name = "Логин")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Логин может содержать только буквы, цифры и символ подчеркивания")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина логина должна быть от 3 до 20 символов")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Поле '{0}' обязательно для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(100, ErrorMessage = "Пароль должен содержать не менее {2} символов.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$", ErrorMessage = "Пароль должен содержать как минимум одну заглавную и одну строчную букву, а также цифру")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Поле '{0}' обязательно для заполнения")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public string? PasswordConfirm { get; set; }
    }
}
