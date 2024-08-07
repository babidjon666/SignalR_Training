using backend.Models;

namespace backend.Interfaces
{
    public interface IChatRepository
    {
        Task<User> GetUserByNameAsync(string userName);
        Task<Chat> GetChatByUsersAsync(User user1, User user2);
        Task AddChatAsync(Chat chat);
        Task SaveChangesAsync();
    }
}