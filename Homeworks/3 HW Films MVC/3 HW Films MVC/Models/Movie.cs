namespace _3_HW_Films_MVC.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Director { get; set; }
        public string? Genre { get; set; }
        public DateTime Date { get; set; }
        public string? PosterPath { get; set; }
        public string? Description { get; set; }
    }
}
