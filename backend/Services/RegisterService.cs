using backend.Data;
using backend.DTOModel;
using backend.Interfaces;
using backend.Models;

namespace backend.Services
{
    public class RegisterService : IRegister
    {
        private readonly IRegisterRepository _registerRepository;

        public RegisterService(IRegisterRepository _registerRepository)
        {
            this._registerRepository = _registerRepository;
        }

        public async Task<Result> Register(string userName, string userSurname, string email, string password)
        {
            if (await _registerRepository.CheckName(userName, email))
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

            await _registerRepository.AddUser(newUser);

            return new Result { Success = true, Message = "Пользователь зарегестрирован!" };
        }
    }
}