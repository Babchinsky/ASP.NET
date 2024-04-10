using _07_HW_09._04._2024.Models;
using Microsoft.EntityFrameworkCore;

namespace _07_HW_09._04._2024.Repositories
{
    public class PostMessageRepository : IPostMessageRepository
    {
        private readonly MessagesContext _context;
        public PostMessageRepository(MessagesContext context)
        {
            _context = context;
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
