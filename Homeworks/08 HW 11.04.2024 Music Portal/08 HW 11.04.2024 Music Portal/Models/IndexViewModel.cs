namespace _08_HW_11._04._2024_Music_Portal.Models
{
    public class IndexViewModel
    {
        public IEnumerable<User> Users { get; set; } = new List<User>();
        public IEnumerable<Song> Songs { get; set; } = new List<Song>();
        public PageViewModel PageViewModel { get; }
        public SortViewModel SortViewModel { get; set; } = new SortViewModel(SortState.NameAsc);

        public IndexViewModel() { }

        public IndexViewModel(IEnumerable<User> users, IEnumerable<Song> songs, SortViewModel sortViewModel, PageViewModel pageViewModel)
        {
            Users = users;
            Songs = songs;
            SortViewModel = sortViewModel;
            PageViewModel = pageViewModel;
        }
    }
}
