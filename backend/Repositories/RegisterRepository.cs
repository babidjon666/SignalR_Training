using backend.Data;
using backend.DTOModel;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class RegisterRepository: BaseRepository, IRegisterRepository
    {
        public RegisterRepository(ApplicationDbContext context): base(context)
        {
        }

        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckName(string userName, string email)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName || u.Email == email);
        }
    }
}