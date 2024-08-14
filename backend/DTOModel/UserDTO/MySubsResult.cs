using backend.Models;

namespace backend.DTOModel.UserDTO
{
    public class MySubsResult
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public IEnumerable<User> Subs { get; set; }
    }
}