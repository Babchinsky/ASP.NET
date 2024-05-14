using System.ComponentModel.DataAnnotations;

namespace _07_HW_09._04._2024.Models
{
    public class User
    {
        public User() 
        {
            this.Messages = new HashSet<Message>();
        }


        public int Id { get; set; }

        [Display(Name="Пользователь")]
        public string Name { get; set; }

        [Display(Name = "Пароль")]
        public string Pwd { get; set; }


        // Навигационное свойство для связи с сообщениями
        [Display(Name = "Сообщения")]
        public ICollection<Message> Messages { get; set; }
    }
}
