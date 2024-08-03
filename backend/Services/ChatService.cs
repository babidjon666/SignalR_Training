using backend.Data;
using backend.DTOModel;
using backend.DTOModel.GetChatsDTO;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ChatService: IChat
    {
        private readonly ApplicationDbContext _context;

        public ChatService(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public async Task<Result> CreateChat(string myName, string friendName)
        {
            var dbUser1 = await _context.Users
                                    .Include(u => u.Chats)
                                    .FirstOrDefaultAsync(u => u.UserName == myName);
            var dbUser2 = await _context.Users
                                    .Include(u => u.Chats)
                                    .FirstOrDefaultAsync(u => u.UserName == friendName);
            
            if (dbUser1 == null || dbUser2 == null){
                var badResponse = new Result{
                    Success = false,
                    Message = "Такого пользователя нет!"
                };

                return badResponse;
            }

            bool chatExists = dbUser1.Chats.Any(c => c.Users.Contains(dbUser2) && c.Users.Contains(dbUser1));

            if (chatExists)
            {
                return new Result
                {
                    Success = false,
                    Message = "Чат между этими пользователями уже существует!"
                };
            }

            var chat = new Chat
            {
                Users = new List<User> { dbUser1, dbUser2 }
            };

            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            dbUser1.Chats.Add(chat);
            dbUser2.Chats.Add(chat);
            await _context.SaveChangesAsync();

            var goodResponse = new Result
                {
                    Success = true,
                    Message = "Чат успешно создан!"
                };
                return goodResponse;
        }

        public async Task<GetChatResult> GetChats(string userName)
        {
            var user = await _context.Users
                                     .Include(u => u.Chats)
                                     .ThenInclude(c => c.Users)
                                     .FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                var badResponse = new GetChatResult{
                    Success = false,
                    Message = "Такого пользователя нет!",
                    Chats = []
                };

                return badResponse;
            }

            var goodResponse = new GetChatResult{
                    Success = true,
                    Message = $"Все чаты пользователя {user.UserName}",
                    Chats = user.Chats
                };

            return goodResponse;
        }
    }
}