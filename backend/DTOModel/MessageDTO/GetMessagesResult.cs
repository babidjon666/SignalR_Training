using backend.Models;

namespace backend.DTOModel.MessageDTO
{
    public class GetMessagesResult
    {
        public Result Result { get; set; }
        public IEnumerable<MessageDTO> Messages { get; set; }
    }
}