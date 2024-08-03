using backend.DTOModel;
using backend.DTOModel.AddChatDTO;
using backend.DTOModel.GetChatsDTO;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController: ControllerBase
    {
        private readonly IChat _chatService;

        public ChatController(IChat _chatService)
        {
            this._chatService = _chatService;
        }

        [HttpGet("Get")]
        public async Task<IEnumerable<Chat>> GetChats([FromQuery] GetChatsRequest request){
            var result = await _chatService.GetChats(request.UserName);

            if (!result.Success){
                return result.Chats;
            }

            return result.Chats;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatRequest request){
            var result = await _chatService.CreateChat(request.Me, request.Friend);

            if (!result.Success){
                return Conflict(result.Message);
            }

            return Ok(result.Message);
        }
    }
}