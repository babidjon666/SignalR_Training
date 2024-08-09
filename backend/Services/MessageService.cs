using backend.DTOModel;
using backend.DTOModel.MessageDTO;
using backend.Interfaces;
using backend.Models;

namespace backend.Services
{
    public class MessageService : IMessage
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository _messageRepository)
        {
            this._messageRepository = _messageRepository;
        }

        public async Task<GetMessagesResult> GetMessages(int chatId)
        {
            var chat = await _messageRepository.FindChat(chatId);

            if (chat == null){
                var badResult = new GetMessagesResult{
                    Result = new Result{
                        Success = false,
                        Message = "Чат не найден"
                    },
                    Messages = []
                };
                return badResult;
            }

            List<MessageDTO> messagesDTO = new List<MessageDTO>();
            foreach(var message in chat.Messages){
                var sender = message.Owner.UserName;
                messagesDTO.Add(
                    new MessageDTO{
                        ChatId = chat.Id,
                        Sender = sender,
                        Text = message.Text,
                        Time = message.DateTime
                    }
                );
            }

            var goodResult = new GetMessagesResult{
                Result = new Result{
                    Success = true,
                    Message = "Чат найден"
                },
                Messages = messagesDTO
            };

            return goodResult;
        }

        public async Task<Result> SendMessage(int chatId, int userId, string text)
        {
            var chat = await _messageRepository.FindChat(chatId);
            var user = await _messageRepository.FindUser(userId);

            if (chat == null){
                var badResult = new Result{
                    Success = false,
                    Message = "Чат не найден"
                };
                    
                return badResult;
            }

            if (user == null){
                var badResult = new Result{
                    Success = false,
                    Message = "Пользователь не найден"
                };
                    
                return badResult;
            }

            if (!user.Chats.Contains(chat)){
                var badResult = new Result{
                    Success = false,
                    Message = "У пользователя нет такого чата"
                };
                    
                return badResult;
            }

            var message = new Message{
                ChatId = chat.Id,
                Chat = chat,
                OwnerId = user.Id,
                Owner = user,
                Text = text
            };

            await _messageRepository.CreateMessage(message);

            user.Messages.Add(message);
            chat.Messages.Add(message);
            await _messageRepository.SaveChangesAsync();

            var goodResult = new Result{
                Success = true,
                Message = "Сообщение отправлено!"
            };

            return goodResult;
        }
    }
}