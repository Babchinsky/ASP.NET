using GuestBookAjax.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AjaxMvcApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            if (_context.Users == null)
                return Problem("Список пользователей пустой!");
            List<User> list = await _context.Users.ToListAsync();
            string response = JsonConvert.SerializeObject(list);
            return Json(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetDetailsById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            string response = JsonConvert.SerializeObject(user);
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> InsertUser(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                string response = "Пользователь успешно добавлен!";
                return Json(response);
            }
            return Problem("Проблемы при добавлении пользователя!");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                string response = "Пользователь успешно изменен!";
                return Json(response);
            }
            return Problem("Проблемы при редактировании пользователя!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Список пользователей пустой!");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            else
            {
                return Problem("Нет такого пользователя!");
            }
            await _context.SaveChangesAsync();
            string response = "Пользователь успешно удален!";
            return Json(response);
        }
    }
}