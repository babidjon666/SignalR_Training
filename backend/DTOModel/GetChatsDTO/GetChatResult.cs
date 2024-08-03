using backend.Models;

namespace backend.DTOModel.GetChatsDTO
{
    public class GetChatResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public IEnumerable<Chat>? Chats { get; set; }
    }
}