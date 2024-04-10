using System.ComponentModel.DataAnnotations;

namespace _07_HW_09._04._2024.Models
{
    public class MessageModel
    {
        [Required]
        [Display(Name ="Сообщение")]
        public string MessageText { get; set; }
    }
}
