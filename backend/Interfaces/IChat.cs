using backend.DTOModel;
using backend.DTOModel.GetChatsDTO;

namespace backend.Interfaces
{
    public interface IChat
    {
        Task<Result> CreateChat(string myName, string friendName);
        Task<GetChatResult> GetChats(string userName);
    }
}