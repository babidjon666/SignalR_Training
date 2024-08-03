using backend.Data;
using backend.DTOModel;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class LoginService : ILogin
    {
        private readonly ApplicationDbContext _context;

        public LoginService(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public async Task<Result> Login(string email, string userPassword)
        {
            var hashedPassword = HashPassword.GetHash(userPassword);

            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == hashedPassword);

            if (dbUser == null){
                return new Result{ Success = false, Message = "Такого пользователя нет!"};
            }

            return new Result{ Success = true, Message = "Пользователь успешно вошел!"};
        }
    }
}