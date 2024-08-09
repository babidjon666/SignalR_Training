using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class MessageRepository: BaseRepository, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext context): base(context)
        {
        }

        public async Task CreateMessage(Message message)
        {
            _context.Messages.Add(message);
            await SaveChangesAsync();
        }

        public async Task<Chat> FindChat(int chatId)
        {
            var dbChat = await _context.Chats
                                    .Include(c => c.Messages)
                                    .ThenInclude(m => m.Owner)
                                    .Include(c => c.Users)
                                    .FirstOrDefaultAsync(c => c.Id == chatId);

            return dbChat;
        }

        public async Task<User> FindUser(int userId)
        {
            var dbUser = await _context.Users
                                    .Include(u => u.Messages)
                                    .Include(u => u.Chats)
                                    .FirstOrDefaultAsync(u => u.Id == userId);
            
            return dbUser;
        }
    }
}