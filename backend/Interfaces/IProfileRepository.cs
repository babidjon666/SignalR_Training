using backend.Enums;
using backend.Models;

namespace backend.Interfaces
{
    public interface IProfileRepository
    {
        Task<User> GetUserById(int id);
        Task<Notify> CreateNotify(User user, NotifyType type, string message);
        Task Sub(User me, User friend);
        Task AddFriend(User me, User sub);
    }
}