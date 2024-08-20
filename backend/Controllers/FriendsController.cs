using backend.DTOModel.Friends.DTO;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class FriendsController: ControllerBase
    {
        private readonly IFriends _friendsService;

        public FriendsController(IFriends _friendsService)
        {
            this._friendsService = _friendsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserWithSubscriptionStatus>>> SearchFriends(string userName, int id)
        {
            var response = await _friendsService.FindFriend(userName, id);

            return Ok(response.Users);
        }
    }
}