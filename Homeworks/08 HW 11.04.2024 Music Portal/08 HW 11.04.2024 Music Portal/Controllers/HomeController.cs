using _08_HW_11._04._2024_Music_Portal.Models;
using _08_HW_11._04._2024_Music_Portal.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace _08_HW_11._04._2024_Music_Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusicPortalContext _context;
        //IUsersRepository _usersRepository;

        public HomeController(MusicPortalContext context)
        {
            _context = context;
        }
        //public HomeController(IUsersRepository usersRepository)
        //{
        //    _usersRepository = usersRepository;
        //}


        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);


            // Проверяем, равен ли userName null, и устанавливаем ViewData["Name"] в null, если да
            ViewData["Name"] = user != null ? user.Login : "Гость";

            var songs = _context.Songs.Include(s => s.Artist).Include(s => s.Genre).ToList();
            var users = _context.Users.ToList();

            // Создание объекта UserAndSongViewModel и заполнение его данными
            var viewModel = new UserAndSongViewModel
            {
                Users = users,
                Songs = songs
            };

            // Передаем пользователя в представление
            ViewData["CurrentUser"] = user;

            // Передача модели в представление
            return View(viewModel);
        }
    }
}
