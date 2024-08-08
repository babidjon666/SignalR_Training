using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace backend.Data
{
    public class UsernameBasedUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}