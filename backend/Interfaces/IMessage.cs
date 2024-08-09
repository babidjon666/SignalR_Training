using backend.DTOModel;
using backend.DTOModel.MessageDTO;
using backend.Models;

namespace backend.Interfaces
{
    public interface IMessage
    {
        Task<GetMessagesResult> GetMessages(int chatId);
        Task<Result> SendMessage(int chatId, int userId, string text);
    }
}