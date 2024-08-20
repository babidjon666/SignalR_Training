using backend.DTOModel.Friends.DTO;

namespace backend.Interfaces
{
    public interface IFriends
    {
        Task<FriendsResult> FindFriend(string name, int id);
    }
}