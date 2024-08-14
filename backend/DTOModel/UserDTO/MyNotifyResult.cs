using backend.Models;

namespace backend.DTOModel.UserDTO
{
    public class MyNotifyResult
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public IEnumerable<Notify> Notifies { get; set; }
    }
}