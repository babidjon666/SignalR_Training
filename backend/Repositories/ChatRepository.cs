using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class ChatRepository : BaseRepository, IChatRepository
    {

        public ChatRepository(ApplicationDbContext context): base(context)
        {
        }

        public async Task<User> GetUserByNameAsync(string userName)
        {
            var dbUser = await _context.Users
                                .Include(u => u.Chats)
                                .ThenInclude(c => c.Users)
                                .FirstOrDefaultAsync(u => u.UserName == userName);

            return dbUser;
        }

        public async Task<Chat> GetChatByUsersAsync(User user1, User user2)
        {
            var chat = await _context.Chats
                                .Include(c => c.Users)
                                .FirstOrDefaultAsync(c => c.Users.Contains(user1) && c.Users.Contains(user2));
            return chat;
        }

        public async Task AddChatAsync(Chat chat)
        {
            _context.Chats.Add(chat);
            await SaveChangesAsync();
        }
    }
}