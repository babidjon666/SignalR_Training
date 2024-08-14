using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class FriendsRepository: BaseRepository, IFriendsRepository
    {
        public FriendsRepository(ApplicationDbContext context): base(context)
        {
            
        }

        public async Task<IEnumerable<User>> SearchUser(string str){
            var lowerStr = str.ToLower();

            var listOfUsers = await _context.Users
                            .Where(u => u.UserName.ToLower().StartsWith(lowerStr) || u.UserSurname.ToLower().StartsWith(lowerStr))
                            .ToListAsync();
            return listOfUsers;
        }
    }
}