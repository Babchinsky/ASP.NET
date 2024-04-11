namespace _08_HW_11._04._2024_Music_Portal.Models
{
    public class Genre
    {
        public Genre() 
        { 
            this.Songs = new HashSet<Song>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
