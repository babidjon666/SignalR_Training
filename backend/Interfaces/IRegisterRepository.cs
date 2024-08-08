using backend.Models;

namespace backend.Interfaces
{
    public interface IRegisterRepository
    {
        Task<bool> CheckName(string userName, string email);
        Task AddUser(User user);
    }
}