using backend.DTOModel;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Interfaces
{
    public interface IRegister
    {
        Task<bool> CheckName(string userName, string email);
        Task<RegisterResult> Register(string userName, string userSurname, string email, string password); 
    }
}