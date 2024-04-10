using _07_HW_09._04._2024.Models;
using _07_HW_09._04._2024.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _07_HW_09._04._2024.Controllers
{
	public class PostMessageController : Controller
	{
		//private readonly MessagesContext _context;

		//public PostMessageController(MessagesContext context)
		//{
		//	_context = context;
		//}
		IPostMessageRepository _postMessageRepository;
		public PostMessageController(IPostMessageRepository postMessageRepository)
		{
			_postMessageRepository = postMessageRepository;
		}

		public IActionResult Index()
		{
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index", "Main");
            }

            var userId = HttpContext.Session.GetInt32("UserId");
			//var userName = _context.Users.FirstOrDefault(u => u.Id == userId);
			var user = _postMessageRepository.GetUserById(Convert.ToInt32(userId));




            // Проверяем, равен ли userName null, и устанавливаем ViewData["Name"] в null, если да
            ViewData["Name"] = user != null ? user.Name : null;


			return View();
		}


		[HttpPost]
		public IActionResult PostMessage(MessageModel model)
		{
			if (ModelState.IsValid)
			{
                var userId = HttpContext.Session.GetInt32("UserId");
                // Получаем пользователя из базы данных по его идентификатору
                var user = _postMessageRepository.GetUserById(Convert.ToInt32(userId));

                if (user != null)
				{
                    // Создайте новый объект сообщения и заполните его данными из модели
                    var message = new Message
                    {
                        Text = model.MessageText,
                        User = user,
                        MessageDate = DateTime.Now,
                    };

                    // Добавьте новое сообщение в базу данных
                    _postMessageRepository.PostMessage(message);
                }

                

				// После успешного сохранения сообщения перенаправьте пользователя на главную страницу или на страницу с сообщениями
				//return RedirectToAction("Index");
                return View("Index", model);
            }

			// Если модель недопустима, верните представление с формой и ошибками валидации
			return View("Index", model);
		}


        public IActionResult Exit()
		{
			HttpContext.Session.Clear();
            return RedirectToAction("Index", "Main"); // Пример перенаправления на главную страницу "Index" контроллера "Home"
        }

    }
}
