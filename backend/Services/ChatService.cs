using backend.Data;
using backend.DTOModel;
using backend.DTOModel.GetChatsDTO;
using backend.Hubs;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ChatService: IChat
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IChatRepository _chatRepository;
        public ChatService(IHubContext<ChatHub> _hubContext, IChatRepository _chatRepository)
        {
            this._hubContext = _hubContext;
            this._chatRepository = _chatRepository;
        }

        public async Task<Result> CreateChat(string myName, string friendName)
        {
            var dbUser1 = await _chatRepository.GetUserByNameAsync(myName);
            var dbUser2 = await _chatRepository.GetUserByNameAsync(friendName);

            if (dbUser1 == null || dbUser2 == null){
                var badResponse = new Result{
                    Success = false,
                    Message = "Такого пользователя нет!"
                };

                return badResponse;
            }

            var chatExists = await _chatRepository.GetChatByUsersAsync(dbUser1, dbUser2);

            if (chatExists != null)
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

            await _chatRepository.AddChatAsync(chat);

            dbUser1.Chats.Add(chat);
            dbUser2.Chats.Add(chat);
            await _chatRepository.SaveChangesAsync();

            await _hubContext.Clients.User(dbUser1.UserName).SendAsync("ReceiveChatUpdate");
            await _hubContext.Clients.User(dbUser2.UserName).SendAsync("ReceiveChatUpdate");

            var goodResponse = new Result
                {
                    Success = true,
                    Message = "Чат успешно создан!"
                };
                
            return goodResponse;
        }

        public async Task<GetChatResult> GetChats(string userName)
        {
            var dbUser = await _chatRepository.GetUserByNameAsync(userName);

            if (dbUser == null)
            {
                var badResponse = new GetChatResult{
                    Success = false,
                    Message = "Такого пользователя нет!",
                    Chats = []
                };

                return badResponse;
            }

            List<ChatDTO> userChats = new List<ChatDTO>();

            foreach(var chat in dbUser.Chats){
                userChats.Add(
                    new ChatDTO{
                        ChatId = chat.Id,
                        User1Name = chat.Users[0].UserName,
                        User2Name = chat.Users[1].UserName
                    }
                );
            }
            
            var goodResponse = new GetChatResult{
                    Success = true,
                    Message = $"Все чаты пользователя {dbUser.UserName}",
                    Chats = userChats
                };

            return goodResponse;
        }
    }
}