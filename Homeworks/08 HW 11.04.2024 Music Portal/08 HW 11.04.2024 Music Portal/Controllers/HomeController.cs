using _08_HW_11._04._2024_Music_Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace _08_HW_11._04._2024_Music_Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusicPortalContext _context;

        public HomeController(MusicPortalContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            //var user = _messagesRepository.GetUserById(Convert.ToInt32(userId));

            // Проверяем, равен ли userName null, и устанавливаем ViewData["Name"] в null, если да
            ViewData["Name"] = user != null ? user.Login : "Гость";

            var songs = _context.Songs.Include(s => s.Artist).Include(s => s.Genre).ToList();

            return View(songs);
        }
    }
}
