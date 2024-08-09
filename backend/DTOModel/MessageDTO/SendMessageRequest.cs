using backend.Models;

namespace backend.DTOModel.MessageDTO
{
    public class SendMessageRequest
    {
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
    }
}