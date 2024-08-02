using backend.DTOModel;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController: ControllerBase
    {
        private readonly IRegister _registerService;

        public RegisterController(IRegister _registerService)
        {
            this._registerService = _registerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _registerService.Register(request.UserName, request.UserSurname, request.Email, request.Password);
            if (!result.Success)
            {
                return Conflict(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpPost]
        public IActionResult SayHi(){
            return Ok("Hi");
        }
    }
}