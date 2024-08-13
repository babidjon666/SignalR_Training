using backend.DTOModel.MessageDTO;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MessageController: ControllerBase
    {
        private readonly IMessage _messageService;

        public MessageController(IMessage _messageService)
        {
            this._messageService = _messageService;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessageInChats([FromQuery] int chatId){
            var result = await _messageService.GetMessages(chatId);

            if (!result.Result.Success){
                return BadRequest(result.Result.Message); 
            }

            return Ok(result.Messages); 
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
            var result = await _messageService.SendMessage(request.ChatId, request.UserId, request.Text);

            if (!result.Success){
                return Conflict(result.Message);
            }

            return Ok(result.Message);
        }
    }
}