namespace _08_HW_11._04._2024_Music_Portal.Models
{
    public class Song
    {
        public int Id { get; set; } 
        public string Title { get; set; } 
        public int Year { get; set; }  
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }
        public User User {  get; set; }
    }
}
