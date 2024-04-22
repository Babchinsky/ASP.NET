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


        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = await _usersRepository.GetFirstOrDefaultUserAsync(userId ?? 0);

            ViewData["Name"] = user?.Login ?? "Гость";

            //var songs = await _context.Songs
            //                         .Include(s => s.Artist)
            //                         .Include(s => s.Genre)
            //                         .ToListAsync();
            var songs = await _songsRepository.GetSongsListAsync();

            //var users = await _context.Users.ToListAsync();
            var users = await _usersRepository.GetUsersListAsync();

            var viewModel = new UserAndSongViewModel
            {
                Users = users,
                Songs = songs
            };

            ViewData["CurrentUser"] = user;

            return View(viewModel);
        }

    }
}
