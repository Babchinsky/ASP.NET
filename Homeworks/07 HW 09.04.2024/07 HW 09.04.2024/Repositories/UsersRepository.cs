using _07_HW_09._04._2024.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace _07_HW_09._04._2024.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MessagesContext _usersContext;

        public UsersRepository(MessagesContext context)
        {
            _usersContext = context;
        }

        public Task AddUserAndSave(User user)
        {
            _usersContext.Users.AddAsync(user);
            return _usersContext.SaveChangesAsync();
        }

        public Task<User> GetUserByName(string name)
        {
            return _usersContext.Users.FirstOrDefaultAsync(a => a.Name == name);
        }

        public async Task<bool> AreUsersNotEmpty()
        {
            return await _usersContext.Users.AnyAsync();
        }

        
    }
}
