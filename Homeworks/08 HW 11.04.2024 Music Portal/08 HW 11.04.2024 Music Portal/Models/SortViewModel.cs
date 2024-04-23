namespace _08_HW_11._04._2024_Music_Portal.Models
{
    public class SortViewModel
    {
        public SortState NameSort { get; set; }
        public SortState ArtistSort { get; set; }
        public SortState GenreSort { get; set; }
        public SortState YearSort { get; set; }
        public SortState Current { get; set; }
        public bool Up { get; set; }

        public SortViewModel(SortState sortOrder)
        {
            NameSort = SortState.NameAsc;
            ArtistSort = SortState.ArtistAsc;
            GenreSort = SortState.GenreAsc;
            YearSort = SortState.YearAsc;
            Up = true;

            if (sortOrder == SortState.ArtistDesc || sortOrder == SortState.NameDesc  || sortOrder == SortState.GenreDesc || sortOrder == SortState.YearDesc)
            {
                Up = false;
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    Current = NameSort = SortState.NameAsc;
                    break;
                case SortState.ArtistAsc:
                    Current = ArtistSort = SortState.ArtistDesc;
                    break;
                case SortState.ArtistDesc:
                    Current = ArtistSort = SortState.ArtistAsc;
                    break;
                case SortState.GenreAsc:
                    Current = GenreSort = SortState.GenreDesc;
                    break;
                case SortState.GenreDesc:
                    Current = GenreSort = SortState.GenreAsc;
                    break;
                case SortState.YearAsc:
                    Current = YearSort = SortState.YearDesc;
                    break;
                case SortState.YearDesc:
                    Current = YearSort = SortState.YearAsc;
                    break;
                default:
                    Current = NameSort = SortState.NameDesc;
                    break;

            }
        }
    }
}
