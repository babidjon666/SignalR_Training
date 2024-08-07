using backend.DTOModel;
using backend.DTOModel.Login.DTO;

namespace backend.Interfaces
{
    public interface ILogin
    {
        Task<LoginResult> Login(string email, string userPassword);
    }
}