using _08_HW_11._04._2024_Music_Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _08_HW_11._04._2024_Music_Portal.Controllers
{
    public class AdminController : Controller
    {
        private readonly MusicPortalContext _context;

        public AdminController(MusicPortalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> VerifyUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.AccessLevel = AccessLevel.VerifiedUser;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(int userId)
        {

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                // Если пользователь не найден, вернем ошибку или выполним необходимые действия
                return NotFound(); // Например, возвращаем ошибку 404 Not Found
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Home");

        }





       

    }
}
