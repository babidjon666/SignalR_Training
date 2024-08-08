using backend.Data;
using backend.DTOModel;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class RegisterService : IRegister
    {
        private readonly ApplicationDbContext _context;

        public RegisterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckName(string userName, string email)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName || u.Email == email);
        }

        public async Task<Result> Register(string userName, string userSurname, string email, string password)
        {
            if (await CheckName(userName, email))
            {
                return new Result { Success = false, Message = "Такой пользователь уже есть!" };
            }

            var hashedPassword = HashPassword.GetHash(password);

            var newUser = new User
            {
                UserName = userName,
                UserSurname = userSurname,
                Email = email,
                Password = hashedPassword
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return new Result { Success = true, Message = "Пользователь зарегестрирован!" };
        }
    }
}