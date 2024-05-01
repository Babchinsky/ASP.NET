using Microsoft.EntityFrameworkCore;

namespace GuestBookAjax.Models
{
    public class DatabaseContext: DbContext
    {
        public DbSet<User> Users { get; set;}
        public DbSet<Message> Messages { get; set;}
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
          : base(options)
        {
            if (Database.EnsureCreated())
            {
                // Инициализация данных по умолчанию
                var user1 = new User { Username = "JohnDoe", Password = "password123" };
                var user2 = new User { Username = "JaneSmith", Password = "password321" };

                var message1 = new Message { Text = "Welcome to the GuestBook!", MessageDate = DateTime.UtcNow, User = user1 };
                var message2 = new Message { Text = "Feel free to leave a message.", MessageDate = DateTime.UtcNow, User = user2 };

                // Добавление пользователей
                Users.AddRange(user1, user2);

                // Добавление сообщений
                Messages.AddRange(message1, message2);

                // Сохранение изменений
                SaveChanges();
            }
        }
    }
}
