using _07_HW_09._04._2024.Models;

namespace _07_HW_09._04._2024.Repositories
{
    public interface IUsersRepository
    {
        Task<List<User>> GetUserList();
        Task<User> GetUserByName(string name);
        Task AddUserAndSave(User user);
    }
}
