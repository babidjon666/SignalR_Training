using backend.Models;

namespace backend.DTOModel.Friends.DTO
{
    public class UserWithSubscriptionStatus
    {
        public User User { get; set; }
        public bool IsSubscribed { get; set; }
    }
}