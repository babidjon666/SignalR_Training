using backend.DTOModel.Friends.DTO;
using backend.Interfaces;

namespace backend.Services
{
    public class FriendsService : IFriends
    {
        private readonly IFriendsRepository _friendsRepository;

        public FriendsService(IFriendsRepository _friendsRepository)
        {
            this._friendsRepository = _friendsRepository;
        }

        public async Task<FriendsResult> FindFriend(string name)
        {
            var listOfUsers = await _friendsRepository.SearchUser(name);

            if (listOfUsers == null){
                var badResult = new FriendsResult{
                    IsSucces = false,
                    Message = "Не найдено",
                    Users = []
                };

                return badResult;
            }

            var goodResult = new FriendsResult{
                IsSucces = true,
                Message = "Найдены!",
                Users = listOfUsers
            };

            return goodResult;
        }
    }
}