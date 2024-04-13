using _08_HW_11._04._2024_Music_Portal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _08_HW_11._04._2024_Music_Portal.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
