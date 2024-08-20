using backend.DTOModel.Friends.DTO;
using backend.Interfaces;
using backend.Models;

namespace backend.Services
{
    public class FriendsService : IFriends
    {
        private readonly IFriendsRepository _friendsRepository;

        public FriendsService(IFriendsRepository _friendsRepository)
        {
            this._friendsRepository = _friendsRepository;
        }

        public async Task<FriendsResult> FindFriend(string name, int id)
        {
            var listOfUsers = await _friendsRepository.SearchUser(name);

            if (listOfUsers == null || !listOfUsers.Any())
            {
                return new FriendsResult
                {
                    IsSucces = false,
                    Message = "Не найдено",
                    Users = new List<UserWithSubscriptionStatus>()
                };
            }

            var usersWithStatus = new List<UserWithSubscriptionStatus>();

            foreach (var user in listOfUsers)
            {
                bool isSubscribed = await _friendsRepository.CheckSub(id, user.Id);
                usersWithStatus.Add(new UserWithSubscriptionStatus
                {
                    User = user,
                    IsSubscribed = isSubscribed
                });
            }

            return new FriendsResult
            {
                IsSucces = true,
                Message = "Найдены!",
                Users = usersWithStatus
            };
        }
    }
}