using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        public LoginRepository(ApplicationDbContext context): base(context)
        {
        }

        public async Task<User> FindUser(string email, string hashedPassword)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == hashedPassword);

            return dbUser;
        }
    }
}