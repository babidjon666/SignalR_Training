using backend.Models;

namespace backend.DTOModel.Friends.DTO
{
    public class FriendsResult
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}