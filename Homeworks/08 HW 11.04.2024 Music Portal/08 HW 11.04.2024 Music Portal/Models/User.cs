namespace _08_HW_11._04._2024_Music_Portal.Models
{
    public enum AccessLevel
    {
        UnverifiedUser,
        VerifiedUser,
        Admin
    }

    public class User
    {
        public User()
        {
            this.Songs = new HashSet<Song>();
        }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
