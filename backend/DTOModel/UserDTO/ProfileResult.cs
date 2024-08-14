using backend.Models;

namespace backend.DTOModel.UserDTO
{
    public class ProfileResult
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
    }
}