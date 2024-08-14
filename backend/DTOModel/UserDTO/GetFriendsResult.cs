using backend.Models;

namespace backend.DTOModel.UserDTO
{
    public class GetFriendsResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<User> Friends { get; set; }
    }
}