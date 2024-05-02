using Microsoft.AspNetCore.Mvc.Rendering;

namespace _08_HW_11._04._2024_Music_Portal.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Artist> artists, int artist, List<Genre> genres, int genre)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            artists.Insert(0, new Artist{ Name = "All", Id = 0 });
            Artists = new SelectList(artists, "Id", "Name", artist);
            SelectedArtist = artist;

            genres.Insert(0, new Genre { Name = "All", Id = 0 });
            Genres = new SelectList(genres, "Id", "Name", genre);
            SelectedGenre = genre;
        }
        public SelectList Artists { get; } // список клубов
        public int SelectedArtist { get; } // выбранный клуб

        public SelectList Genres { get; } // список клубов
        public int SelectedGenre { get; } // выбранный клуб

    }
}