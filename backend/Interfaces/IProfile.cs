using backend.DTOModel;
using backend.DTOModel.UserDTO;
using backend.Models;

namespace backend.Interfaces
{
    public interface IProfile
    {
        Task<ProfileResult> FindUser(int id);
        Task<MySubsResult> GetMySubs(int id); 
        Task<MyNotifyResult> GetMyNotify(int id); 
        Task<SubscribeResult> Subscribe(int myId, int friendId);
        Task<Result> AcceptFriend(int myId, int subId);
        Task<GetFriendsResult> GetFriend(int userId);
    }
}