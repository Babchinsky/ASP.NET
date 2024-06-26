﻿using _08_HW_11._04._2024_Music_Portal.Models;
using _08_HW_11._04._2024_Music_Portal.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace _08_HW_11._04._2024_Music_Portal.Controllers
{
    public class AccountController: Controller
    {
        IUsersRepository _usersRepository;

        public AccountController(IUsersRepository usersRepository) 
        {
            _usersRepository = usersRepository;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                //if (_context.Users.ToList().Count == 0)
                if (await _usersRepository.AreUsersNotEmptyAsync() == false)
                    {
                    ModelState.AddModelError("", "Неверный логин или пароль!");
                    return View(logon);
                }
                var users = await _usersRepository.GetUsersByLoginAsync(logon.Login);

                if (users.Count == 0)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль!");
                    return View(logon);
                }

                var user = users.First();
                string? salt = user.Salt;


                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + logon.Password);

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (user.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Неверный логин или пароль!");
                    return View(logon);
                }
                HttpContext.Session.SetInt32("UserId", user.Id);
                return RedirectToAction("Index", "Home");
            }
            return View(logon);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel reg)
        {
            if (ModelState.IsValid)
            {
                // Проверяем, существует ли пользователь с таким же логином
                //var existingUser = _context.Users.FirstOrDefault(u => u.Login == reg.Login);
                var existingUser = await _usersRepository.GetUserByLoginAsync(reg.Login);


                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует.");
                    return View(reg);
                }

                // Если пользователь с таким логином не найден, создаем нового пользователя
                User user = new User();
                user.Login = reg.Login;
                user.AccessLevel = AccessLevel.UnverifiedUser;

                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                user.Password = hash.ToString();
                user.Salt = salt;
                //_context.Users.Add(user);
                //_context.SaveChanges();
                _usersRepository.AddUserAndSaveAsync(user);

                return RedirectToAction("Login");
            }

            return View(reg);
        }
    }
}
