namespace GuestBookAjax.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime MessageDate { get; set; }
        public User? User { get; set; }
    }
}
