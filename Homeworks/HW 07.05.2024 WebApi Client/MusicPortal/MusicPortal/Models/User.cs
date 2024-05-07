using System.ComponentModel.DataAnnotations;
namespace MusicPortal.Models
{
    public enum AccessLevel
    {
        //[Display(Name = "Не подтвержденный пользователь")]
        UnverifiedUser,

        //[Display(Name = "Подтвержденный пользователь")]
        VerifiedUser,

        //[Display(Name = "Администратор")]
        Admin
    }
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public AccessLevel AccessLevel { get; set; }
    }
}
