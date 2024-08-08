using backend.DTOModel;

namespace backend.Interfaces
{
    public interface IRegister
    {
        Task<Result> Register(string userName, string userSurname, string email, string password); 
    }
}