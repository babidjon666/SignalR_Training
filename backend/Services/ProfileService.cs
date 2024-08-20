using backend.DTOModel;
using backend.DTOModel.UserDTO;
using backend.Hubs;
using backend.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace backend.Services
{
    public class ProfileService : IProfile
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IHubContext<ChatHub> _hubContext;

        public ProfileService(IProfileRepository _profileRepository, IHubContext<ChatHub> _hubContext)
        {
            this._profileRepository = _profileRepository;
            this._hubContext = _hubContext;
        }

        public async Task<Result> AcceptFriend(int myId, int subId)
        {
            var me = await _profileRepository.GetUserById(myId);
            var sub = await _profileRepository.GetUserById(subId);

            if (me == null || sub == null){
                var badResponse = new Result{
                    Success = false,
                    Message = "Пользователь не найден"
                };

                return badResponse;
            }

            if (!me.Subscribers.Contains(sub)){
                var badResponse = new Result{
                    Success = false,
                    Message = "Пользователь не подписан на вас!"
                };

                return badResponse;
            }

            await _profileRepository.AddFriend(me, sub);
        
            var goodResponse = new Result{
                Success = true,
                Message = $"Пользователь {me.UserName} добавил в друзья {sub.UserName}"
            };

            var notify1 = await _profileRepository.CreateNotify(me, Enums.NotifyType.NewFriend, "У вас новый друг!");
            
            if (notify1 != null){
                //await _hubContext.Clients.User(me.UserName).SendAsync("NewFriend", notify1);
            }

            var notify2 = await _profileRepository.CreateNotify(sub, Enums.NotifyType.NewFriend, "У вас новый друг!");

            if (notify2 != null){
                //await _hubContext.Clients.User(sub.UserName).SendAsync("NewFriend", notify2);
            }
            return goodResponse;
        }

        public async Task<bool> CheckSub(int myId, int subId)
        {
            var me = await _profileRepository.GetUserById(myId);
            var friend = await _profileRepository.GetUserById(subId);

            if (friend.Subscribers.Contains(me)){
                return true;
            }
            return false;
        }

        public async Task<ProfileResult> FindUser(int id)
        {
            var dbUser = await _profileRepository.GetUserById(id);

            if (dbUser == null){
                var badResponse = new ProfileResult{
                    IsSucces = false,
                    Message = "Пользователь не найден"
                };

                return badResponse;
            }

            var goodResponse = new ProfileResult{
                IsSucces = true,
                Message = "Пользователь найден",
                User = dbUser
            };

            return goodResponse;
        }

        public async Task<GetFriendsResult> GetFriend(int userId)
        {
            var dbUser = await _profileRepository.GetUserById(userId);

            if (dbUser == null){
                var badResponse = new GetFriendsResult{
                    Success = false,
                    Message = "Пользователь не найден"
                };

                return badResponse;
            }

            var goodResponse = new GetFriendsResult{
                Success = true,
                Message = "Список друзей",
                Friends = dbUser.Friends
            };

            return goodResponse;
        }

        public async Task<MyNotifyResult> GetMyNotify(int id)
        {
            var dbUser = await _profileRepository.GetUserById(id);

            if (dbUser == null){
                var badResponse = new MyNotifyResult{
                    IsSucces = false,
                    Message = "Пользователь не найден"
                };

                return badResponse;
            }

            var goodResponse = new MyNotifyResult{
                IsSucces = true,
                Message = "Мои уведомления",
                Notifies = dbUser.Notifies
            };

            return goodResponse;
        }

        public async Task<MySubsResult> GetMySubs(int id)
        {
            var dbUser = await _profileRepository.GetUserById(id);

            if (dbUser == null){
                var badResponse = new MySubsResult{
                    IsSucces = false,
                    Message = "Пользователи не найдены!"
                };

                return badResponse;
            }

            var goodResponse = new MySubsResult{
                IsSucces = true,
                Message = "Мои подписчики",
                Subs = dbUser.Subscribers
            };

            return goodResponse;
        }

        public async Task<SubscribeResult> Subscribe(int myId, int friendId)
        {
            var me = await _profileRepository.GetUserById(myId);
            var friend = await _profileRepository.GetUserById(friendId);

            if (me == null || friend == null){
                var badResponse = new SubscribeResult{
                    IsSucces = false,
                    Message = "Пользователи не найдены"
                };

                return badResponse;
            }

            if (myId == friendId){
                var badResponse = new SubscribeResult{
                    IsSucces = false,
                    Message = "Вы не моежете подписаться на себя!"
                };

                return badResponse;
            }

            if (friend.Subscribers.Contains(me)){
                var badResponse = new SubscribeResult{
                    IsSucces = false,
                    Message = $"Вы уже подписаны на {friend.UserName}"
                };
            
                return badResponse;
            }
            var notify = await _profileRepository.CreateNotify(friend, Enums.NotifyType.NewSub, "У вас новый подписчик!");
            
            if (notify != null){
                await _profileRepository.Sub(me, friend);
            }

            var goodResponse = new SubscribeResult{
                IsSucces = true,
                Message = $"Пользователь {me.UserName} подписался на {friend.UserName}"
            };

            return goodResponse;
        }
    }
}