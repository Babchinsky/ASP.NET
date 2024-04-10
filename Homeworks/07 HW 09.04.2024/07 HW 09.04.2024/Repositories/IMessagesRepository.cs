using _07_HW_09._04._2024.Models;

namespace _07_HW_09._04._2024.Repositories
{
    public interface IMessagesRepository
    {
        Task<List<Message>> GetMessageList();
    }
}
