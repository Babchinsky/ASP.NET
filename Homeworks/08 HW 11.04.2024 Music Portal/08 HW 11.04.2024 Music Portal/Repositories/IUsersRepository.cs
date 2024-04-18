using _08_HW_11._04._2024_Music_Portal.Models;

namespace _08_HW_11._04._2024_Music_Portal.Repositories
{
    public interface IUsersRepository
    {
        Task<bool> AreUsersNotEmptyAsync();
        Task<List<User>> GetUsersByLoginAsync(string login);
        Task<User> GetUserByLoginAsync(string login);
        Task AddUserAndSaveAsync(User user);

        Task<User> FindUserByIdAsync(int userId);
        Task SaveChangesAsync();
        Task RemoveUserAndSaveAsync(User user);
    }
}
