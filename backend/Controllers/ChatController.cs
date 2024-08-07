using backend.DTOModel;
using backend.DTOModel.AddChatDTO;
using backend.DTOModel.GetChatsDTO;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChat _chatService;

        public ChatController(IChat chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<ChatDTO>>> GetChats([FromQuery] string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return BadRequest("User name is required.");
            }

            var result = await _chatService.GetChats(userName);

            if (!result.Success)
            {
                return BadRequest(result.Message); // Используйте BadRequest для неуспешных ответов
            }

            return Ok(result.Chats); // Используйте Ok для успешных ответов
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Me) || string.IsNullOrWhiteSpace(request.Friend))
            {
                return BadRequest("Invalid request.");
            }

            var result = await _chatService.CreateChat(request.Me, request.Friend);

            if (!result.Success)
            {
                return Conflict(result.Message); // Используйте Conflict для конфликтов
            }

            return Ok(result.Message); // Используйте Ok для успешных ответов
        }
    }
}