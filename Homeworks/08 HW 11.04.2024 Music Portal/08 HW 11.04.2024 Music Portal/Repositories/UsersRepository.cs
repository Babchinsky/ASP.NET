using _08_HW_11._04._2024_Music_Portal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace _08_HW_11._04._2024_Music_Portal.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MusicPortalContext _context;

        public UsersRepository(MusicPortalContext context)
        {
            _context = context;
        }

        public async Task<bool> AreUsersNotEmptyAsync()
        {
            return await _context.Users.AnyAsync();
        }

        public async Task<List<User>> GetUsersByLoginAsync(string login)
        {
            return await _context.Users.Where(a => a.Login == login).ToListAsync();
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task AddUserAndSaveAsync(User user)
        {
            _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> FindUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId); 
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserAndSaveAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetFirstOrDefaultUserAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
