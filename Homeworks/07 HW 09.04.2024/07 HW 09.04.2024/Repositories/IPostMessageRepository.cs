using _07_HW_09._04._2024.Models;

namespace _07_HW_09._04._2024.Repositories
{
    public interface IPostMessageRepository
    {
        User GetUserById(int id);
        Task PostMessage(Message message);
    }
}
