namespace MusicPortal.Models
{
    public enum AccessLevel
    {
        UnverifiedUser,
        VerifiedUser,
        Admin
    }
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }
}
