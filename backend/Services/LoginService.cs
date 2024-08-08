using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Data;
using backend.DTOModel;
using backend.DTOModel.Login.DTO;
using backend.Interfaces;
using backend.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services
{
    public class LoginService : ILogin
    {
        private readonly ILoginRepository _loginRepository;
        private readonly JwtSettings _jwtSettings;

        public LoginService(IOptions<JwtSettings> jwtSettings, ILoginRepository _loginRepository)
        {
            this._loginRepository = _loginRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<LoginResult> Login(string email, string userPassword)
        {
            var hashedPassword = HashPassword.GetHash(userPassword);

            var dbUser = await _loginRepository.FindUser(email, hashedPassword);
            
            if (dbUser == null){
                return new LoginResult{ 
                    Result = new Result{
                        Success = false, 
                        Message = "Такого пользователя нет!"
                    }
                };
            }

            var token = GenerateJwtToken(dbUser);

            return new LoginResult{ 
                Id = dbUser.Id,
                UserName = dbUser.UserName,
                UserSurname = dbUser.UserSurname,
                Email = dbUser.Email,
                Token = token,
                Result = new Result{
                    Success = true, 
                    Message = "Пользователь успешно вошел!"
                }
            };
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}