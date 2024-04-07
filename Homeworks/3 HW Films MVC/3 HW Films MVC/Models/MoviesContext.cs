using Microsoft.EntityFrameworkCore;


namespace _3_HW_Films_MVC.Models
{
    public class MoviesContext: DbContext
    {
        public DbSet<Movie> movies { get; set; }

        public MoviesContext(DbContextOptions<MoviesContext> options)
           : base(options)
        {
            if (Database.EnsureCreated())
            {
                movies.Add(new Movie
                {
                    Title = "The Shawshank Redemption",
                    Director = "Frank Darabont",
                    Genre = "Drama",
                    Date = new DateTime(1994, 10, 14),
                    PosterPath = "https://m.media-amazon.com/images/I/71715eBi1sL._AC_SL1000_.jpg",
                    Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency."
                });

                movies.Add(new Movie
                {
                    Title = "The Godfather",
                    Director = "Francis Ford Coppola",
                    Genre = "Crime",
                    Date = new DateTime(1972, 3, 24),
                    PosterPath = "https://m.media-amazon.com/images/I/81ofV3fBLOL._AC_SL1500_.jpg",
                    Description = "An organized crime dynasty's aging patriarch transfers control of his clandestine empire to his reluctant son."
                });

                movies.Add(new Movie
                {
                    Title = "The Godfather: Part II",
                    Director = "Francis Ford Coppola",
                    Genre = "Crime",
                    Date = new DateTime(1974, 12, 20),
                    PosterPath = "https://m.media-amazon.com/images/I/41N2vp6wUiL._AC_.jpg",
                    Description = "The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate."
                });

                movies.Add(new Movie
                {
                    Title = "The Dark Knight",
                    Director = "Christopher Nolan",
                    Genre = "Action",
                    Date = new DateTime(2008, 7, 18),
                    PosterPath = "https://m.media-amazon.com/images/I/51rF2-tvXVL._AC_.jpg",
                    Description = "When the menace known as The Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice."
                });

                movies.Add(new Movie
                {
                    Title = "12 Angry Men",
                    Director = "Sidney Lumet",
                    Genre = "Drama",
                    Date = new DateTime(1957, 4, 10),
                    PosterPath = "https://m.media-amazon.com/images/I/51RcerL7StL._AC_.jpg",
                    Description = "A jury holdout attempts to prevent a miscarriage of justice by forcing his colleagues to reconsider the evidence."
                });

                movies.Add(new Movie
                {
                    Title = "Schindler's List",
                    Director = "Steven Spielberg",
                    Genre = "Biography",
                    Date = new DateTime(1994, 2, 4),
                    PosterPath = "https://m.media-amazon.com/images/I/71EtVRky4eL._AC_SL1000_.jpg",
                    Description = "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis."
                });

                movies.Add(new Movie
                {
                    Title = "The Lord of the Rings: The Return of the King",
                    Director = "Peter Jackson",
                    Genre = "Adventure",
                    Date = new DateTime(2003, 12, 17),
                    PosterPath = "https://m.media-amazon.com/images/I/71fN6GnTcGL._AC_SL1000_.jpg",
                    Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring."
                });

                movies.Add(new Movie
                {
                    Title = "Pulp Fiction",
                    Director = "Quentin Tarantino",
                    Genre = "Crime",
                    Date = new DateTime(1994, 10, 14),
                    PosterPath = "https://m.media-amazon.com/images/I/71iQzfnYGeL._AC_SL1000_.jpg",
                    Description = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption."
                });

                movies.Add(new Movie
                {
                    Title = "The Lord of the Rings: The Fellowship of the Ring",
                    Director = "Peter Jackson",
                    Genre = "Adventure",
                    Date = new DateTime(2001, 12, 19),
                    PosterPath = "https://m.media-amazon.com/images/I/51klvNNCWoL._AC_.jpg",
                    Description = "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron."
                });

                movies.Add(new Movie
                {
                    Title = "Fight Club",
                    Director = "David Fincher",
                    Genre = "Drama",
                    Date = new DateTime(1999, 10, 15),
                    PosterPath = "https://m.media-amazon.com/images/I/81Luju2cHuL._AC_SL1500_.jpg",
                    Description = "An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much, much more."
                });
                SaveChanges();
            }

        }
    }
}
