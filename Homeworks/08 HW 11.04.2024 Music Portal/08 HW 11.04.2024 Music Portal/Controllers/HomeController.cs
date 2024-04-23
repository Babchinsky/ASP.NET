using _08_HW_11._04._2024_Music_Portal.Models;
using _08_HW_11._04._2024_Music_Portal.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace _08_HW_11._04._2024_Music_Portal.Controllers
{
    public class HomeController : Controller
    {
        //private readonly MusicPortalContext _context;
        IUsersRepository _usersRepository;
        ISongsRepository _songsRepository;

        //public HomeController(MusicPortalContext context)
        //{
        //    _context = context;
        //}
        public HomeController(IUsersRepository usersRepository, ISongsRepository songsRepository)
        {
            _usersRepository = usersRepository;
            _songsRepository = songsRepository;
        }


        public async Task<IActionResult> Index(SortState sortOrder = SortState.NameAsc)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = await _usersRepository.GetFirstOrDefaultUserAsync(userId ?? 0);

            ViewData["Name"] = user?.Login ?? "Гость";

            //var songs = await _context.Songs
            //                         .Include(s => s.Artist)
            //                         .Include(s => s.Genre)
            //                         .ToListAsync();
            //var songs = await _songsRepository.GetSongsListAsync();

            //var users = await _context.Users.ToListAsync();
            var users = await _usersRepository.GetUsersListAsync();

            //var viewModel = new IndexViewModel
            //{
            //    Users = users,
            //    Songs = songs
            //};

            ViewData["CurrentUser"] = user;

            //return View(viewModel);


            //// Подключаем Genre и Artist для Song
            //IQueryable<Song>? songs = _context.Songs
            //                                  .Include(x => x.Genre)   // Включаем Genre
            //                                  .Include(x => x.Artist); // Включаем Artist

            //IQueryable<User>? users = _context.Users;

            //songs = sortOrder switch
            // {
            //     SortState.NameDesc => songs.OrderByDescending(s => s.Title),
            //     SortState.ArtistAsc => songs.OrderBy(s => s.Artist!.Name),
            //     SortState.ArtistDesc => songs.OrderByDescending(s => s.Artist!.Name),
            //     SortState.GenreAsc => songs.OrderBy(s => s.Genre!.Name),
            //     SortState.GenreDesc => songs.OrderByDescending(s => s.Genre!.Name),
            //     SortState.YearAsc => songs.OrderBy(s => s.Year),
            //     SortState.YearDesc => songs.OrderByDescending(s => s.Year),
            //     _ => songs.OrderBy(s => s.Title),
            // };


            var songs = await _songsRepository.GetSongsListAsync(); // Асинхронный вызов

            //// Применяем сортировку в зависимости от sortOrder
            //songs = sortOrder switch
            //{
            //    SortState.NameDesc => songs.OrderByDescending(s => s.Title),
            //    SortState.ArtistAsc => songs.OrderBy(s => s.Artist?.Name),
            //    SortState.ArtistDesc => songs.OrderByDescending(s => s.Artist?.Name),
            //    SortState.GenreAsc => songs.OrderBy(s => s.Genre?.Name),
            //    SortState.GenreDesc => songs.OrderByDescending(s => s.Genre?.Name),
            //    SortState.YearAsc => songs.OrderBy(s => s.Year),
            //    SortState.YearDesc => songs.OrderByDescending(s => s.Year),
            //    _ => songs.OrderBy(s => s.Title),
            //};

            var orderedSongs = sortOrder switch
            {
                SortState.NameDesc => songs.OrderByDescending(s => s.Title),
                SortState.ArtistAsc => songs.OrderBy(s => s.Artist?.Name),
                SortState.ArtistDesc => songs.OrderByDescending(s => s.Artist?.Name),
                SortState.GenreAsc => songs.OrderBy(s => s.Genre?.Name),
                SortState.GenreDesc => songs.OrderByDescending(s => s.Genre?.Name),
                SortState.YearAsc => songs.OrderBy(s => s.Year),
                SortState.YearDesc => songs.OrderByDescending(s => s.Year),
                _ => songs.OrderBy(s => s.Title),
            };
            var songList = orderedSongs.ToList(); // Преобразуем в List после сортировки

            //IndexViewModel viewModel = new IndexViewModel
            //{
            //    Users = await users.ToListAsync(),
            //    Songs = await songs.ToListAsync(),
            //    SortViewModel = new SortViewModel(sortOrder)
            //};

            IndexViewModel viewModel = new IndexViewModel
            {
                Users = await _usersRepository.GetUsersListAsync(),
                Songs = songList, // Используем songList после преобразования
                SortViewModel = new SortViewModel(sortOrder),
            };

            return View(viewModel);
        }

    }
}
