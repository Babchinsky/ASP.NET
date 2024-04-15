using Microsoft.AspNetCore.Mvc;

namespace _08_HW_11._04._2024_Music_Portal.Controllers
{
    public class DataController : Controller
    {
        public IActionResult AddGenre()
        {
            return View("AddGenre");
        }
    }
}
