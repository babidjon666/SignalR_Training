using backend.DTOModel;

namespace backend.Interfaces
{
    public interface ILogin
    {
        Task<Result> Login(string email, string userPassword);
    }
}