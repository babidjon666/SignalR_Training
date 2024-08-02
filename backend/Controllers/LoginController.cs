using backend.DTOModel;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController: ControllerBase
    {
        private readonly ILogin _loginService;

        public LoginController(ILogin _loginService)
        {
            this._loginService = _loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request){
            var result = await _loginService.Login(request.Email, request.Password);

            if (!result.Success){
                return Conflict(result.Message);
            }

            return Ok(result.Message);
        }
    }
}