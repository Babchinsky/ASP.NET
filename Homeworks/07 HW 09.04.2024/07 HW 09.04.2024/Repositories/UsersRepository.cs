using _07_HW_09._04._2024.Models;
using Microsoft.EntityFrameworkCore;

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
            throw new NotImplementedException();
        }

        public Task<User> GetUserByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetUserList()
        {
            //return await _usersContext.Users.AnyAsync();
            throw new NotImplementedException();
        }
    }
}
