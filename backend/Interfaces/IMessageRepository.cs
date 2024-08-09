using backend.Models;

namespace backend.Interfaces
{
    public interface IMessageRepository
    {
        Task<Chat> FindChat(int chatId);
        Task<User> FindUser(int userId);
        Task CreateMessage(Message message);
        Task SaveChangesAsync();
    }
}