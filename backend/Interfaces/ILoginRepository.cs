using backend.Models;

namespace backend.Interfaces
{
    public interface ILoginRepository
    {
        Task<User> FindUser(string userName, string hashedPassword);
    }
}