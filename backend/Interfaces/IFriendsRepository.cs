using backend.Models;

namespace backend.Interfaces
{
    public interface IFriendsRepository
    {
         Task<IEnumerable<User>> SearchUser(string str);
    }
}