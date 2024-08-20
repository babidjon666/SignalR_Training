using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ProfileController: ControllerBase
    {
        private readonly IProfile _profileService;

        public ProfileController(IProfile _profileService)
        {
            this._profileService = _profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id){
            var response = await _profileService.FindUser(id);

            if (!response.IsSucces){
                return Conflict(response.Message);
            }

            return Ok(new {
                response.User.Id,
                response.User.UserName,
                response.User.UserSurname,
                response.User.Email
            });
        }

        [HttpPost("Subscribe")]
        public async Task<IActionResult> Subscribe(int myID, int friendID){
            var response = await _profileService.Subscribe(myID, friendID);

            if (!response.IsSucces){
                return Conflict(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("CheckSub")]
        public async Task<IActionResult> CheckSub(int myID, int friendID){
            var response = await _profileService.CheckSub(myID, friendID);

            if (response){
                return Conflict("Вы уже подписаны на пользователя!");
            }
            return Ok("Подписка");
        }

        [HttpGet("GetSubs")]
        public async Task<ActionResult<IEnumerable<User>>> GetSubs(int id){
            var response = await _profileService.GetMySubs(id);

            if (!response.IsSucces){
                return Conflict(response.Message);
            }

            return Ok(response.Subs);
        }

        [HttpGet("GetNotify")]
        public async Task<ActionResult<IEnumerable<Notify>>> GetNotify(int id){
            var response = await _profileService.GetMyNotify(id);

            if (!response.IsSucces){
                return Conflict(response.Message);
            }

            return Ok(response.Notifies);
        }

        [HttpPost("AcceptFriend")]
        public async Task<ActionResult> AcceptFriend (int myId, int friendId){
            var response = await _profileService.AcceptFriend(myId, friendId);

            if (!response.Success){
                return Conflict(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("GetFriends")]
        public async Task<ActionResult<IEnumerable<User>>> GetFriends(int id){
            var response = await _profileService.GetFriend(id);

            if (!response.Success){
                return Conflict(response.Message);
            }

            return Ok(response.Friends);
        }
    }
}