namespace _08_HW_11._04._2024_Music_Portal.Models
{
    public class IndexViewModel
    {
        public IEnumerable<User> Users { get; set; } = new List<User>();
        public IEnumerable<Song> Songs { get; set; } = new List<Song>();
        public SortViewModel SortViewModel { get; set; } = new SortViewModel(SortState.NameAsc);
    }
}
