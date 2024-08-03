using backend.DTOModel;

namespace backend.Interfaces
{
    public interface IRegister
    {
        Task<bool> CheckName(string userName, string email);
        Task<Result> Register(string userName, string userSurname, string email, string password); 
    }
}