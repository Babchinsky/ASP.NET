using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _07_HW_09._04._2024.Models;

namespace _07_HW_09._04._2024.Controllers
{
    public class MainController : Controller
    {
        private readonly MessagesContext _context;

        public MainController(MessagesContext context)
        {
            _context = context;
        }



		// GET: Messages
		public async Task<IActionResult> Index()
		{
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("Index", "PostMessage");
            }
            



            return View(await _context.Messages.Include(m => m.User).ToListAsync());
			//Добавив Include(m => m.User), вы указываете Entity Framework загрузить связанный объект пользователя для каждого сообщения.
		}

    }
}
