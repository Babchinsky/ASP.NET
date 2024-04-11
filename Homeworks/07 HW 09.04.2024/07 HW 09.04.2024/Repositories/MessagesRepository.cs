using _07_HW_09._04._2024.Models;
using Microsoft.EntityFrameworkCore; // Добавьте эту директиву, если её нет

namespace _07_HW_09._04._2024.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly MessagesContext _context;

        public MessagesRepository(MessagesContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetMessageList()
        {
            return await _context.Messages.Include(m => m.User).ToListAsync();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public async Task PostMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }
    }
}
