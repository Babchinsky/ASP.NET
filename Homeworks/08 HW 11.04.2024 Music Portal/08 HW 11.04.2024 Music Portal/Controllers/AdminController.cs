using _08_HW_11._04._2024_Music_Portal.Models;
using _08_HW_11._04._2024_Music_Portal.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _08_HW_11._04._2024_Music_Portal.Controllers
{
    public class AdminController : Controller
    {
        //private readonly MusicPortalContext _context;
        IUsersRepository _usersRepository;

        public AdminController(MusicPortalContext context, IUsersRepository usersRepository)
        {
            //_context = context;
            _usersRepository = usersRepository;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> VerifyUser(int userId)
        {

            //var user = await _context.Users.FindAsync(userId);
            var user = await _usersRepository.FindUserByIdAsync(userId);
            if (user != null)
            {
                user.AccessLevel = AccessLevel.VerifiedUser;
                //await _context.SaveChangesAsync();
                await _usersRepository.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(int userId)
        {

            //var user = await _context.Users.FindAsync(userId);
            var user = await _usersRepository.FindUserByIdAsync(userId);

            if (user == null)
            {
                // Если пользователь не найден, вернем ошибку или выполним необходимые действия
                return NotFound(); // Например, возвращаем ошибку 404 Not Found
            }

            //_context.Users.Remove(user);
            //await _context.SaveChangesAsync();
            await _usersRepository.RemoveUserAndSaveAsync(user);
            

            return RedirectToAction("Index", "Home");

        }





       

    }
}
