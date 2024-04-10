using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

using _07_HW_09._04._2024.Models;
using _07_HW_09._04._2024.Repositories;



namespace _07_HW_09._04._2024.Controllers
{
    public class AccountController : Controller
    {
        private readonly MessagesContext _context;

        public AccountController(MessagesContext context)
        {
            _context = context;
        }

        public ActionResult Login()
        {
            return View();
        }

       


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel) // Был параметр login и он пересекался с полем Login. Из-за этого были проблемы
        {
            if (ModelState.IsValid)
            {
                if (await _context.Users.AnyAsync())
                {
                    var user = await _context.Users.FirstOrDefaultAsync(a => a.Name == loginModel.Login);
                    if (user != null)
                    {
                        if (loginModel.Password == user.Pwd)
                        {
                            HttpContext.Session.SetString("UserId", user.Id.ToString());
                            return RedirectToAction("Index", "PostMessage");
                        }
                        
                    }
                    ModelState.AddModelError("", "Логин или пользователь введён не верно!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Модель не валидна!");
            }
            
            return View(loginModel);
        }


        public ActionResult Register()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel reg)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Name = reg.Login;
                user.Pwd = reg.Password;
                
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(reg);
        }





        //public User GetUserById(string userId)
        //{
        //    if (int.TryParse(userId, out int id))
        //    {
        //        return _context.Users.FirstOrDefault(u => u.Id == id);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
