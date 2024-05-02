using _08_HW_11._04._2024_Music_Portal.Models;
using _08_HW_11._04._2024_Music_Portal.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing.Printing;

namespace _08_HW_11._04._2024_Music_Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusicPortalContext _context;
        IUsersRepository _usersRepository;
        ISongsRepository _songsRepository;

        //public HomeController(MusicPortalContext context)
        //{
        //    _context = context;
        //}
        public HomeController(MusicPortalContext context, IUsersRepository usersRepository, ISongsRepository songsRepository)
        {
            _context = context;
            _usersRepository = usersRepository;
            _songsRepository = songsRepository;
        }


        public async Task<IActionResult> Index(SortState sortOrder = SortState.NameAsc, int page = 1)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = await _usersRepository.GetFirstOrDefaultUserAsync(userId ?? 0);

            ViewData["Name"] = user?.Login ?? "Гость";

            var users = await _usersRepository.GetUsersListAsync();

            
            ViewData["CurrentUser"] = user;

            


            //var songs = await _songsRepository.GetSongsListAsync(); // Асинхронный вызов
            IQueryable<Song> songs = _context.Songs.Include(s => s.Artist).Include(s => s.Genre);


            songs = sortOrder switch
            {
                SortState.NameDesc => songs.OrderByDescending(s => s.Title),
                SortState.ArtistAsc => songs.OrderBy(s => s.Artist.Name),
                SortState.ArtistDesc => songs.OrderByDescending(s => s.Artist.Name),
                SortState.GenreAsc => songs.OrderBy(s => s.Genre.Name),
                SortState.GenreDesc => songs.OrderByDescending(s => s.Genre.Name),
                SortState.YearAsc => songs.OrderBy(s => s.Year),
                SortState.YearDesc => songs.OrderByDescending(s => s.Year),
                _ => songs.OrderBy(s => s.Title),
            };
            //var songList = orderedSongs.ToList(); // Преобразуем в List после сортировки

            int pageSize = 5;   // количество элементов на странице
            var count = await songs.CountAsync();
            var items = await songs.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);


            IndexViewModel viewModel = new IndexViewModel(
                users: await _usersRepository.GetUsersListAsync(),
                songs: items, // Используйте 'items', которые содержат текущую страницу песен
                sortViewModel: new SortViewModel(sortOrder),
                pageViewModel: pageViewModel
             );
            return View(viewModel);
        }

    }
}
