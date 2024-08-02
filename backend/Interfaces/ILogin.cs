using backend.DTOModel;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Interfaces
{
    public interface ILogin
    {
        Task<LoginResult> Login(string email, string userPassword);
    }
}