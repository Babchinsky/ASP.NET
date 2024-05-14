using System.ComponentModel.DataAnnotations;

namespace _07_HW_09._04._2024.Models
{
    public class Message
    {
        
        public int Id { get; set; }

		[Display(Name = "Сообщение")]
		public string Text { get; set; }

		[Display(Name = "Дата сообщения")]
		public DateTime MessageDate { get; set; }


		[Display(Name = "Пользователь")]
		// Навигационное свойство для связи с пользователем
		public User User { get; set; }
    }
}
