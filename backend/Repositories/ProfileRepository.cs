using System.Net;
using backend.Data;
using backend.Enums;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class ProfileRepository: BaseRepository, IProfileRepository
    {
        public ProfileRepository(ApplicationDbContext context): base(context)
        {
            
        }

        public async Task<Notify> CreateNotify(User user, NotifyType type, string message)
        {
            var notify = new Notify{
                NotifyType = type,
                Text = message,
                IsReaded = false,
                Time = DateTime.Now,
                UserId = user.Id
            };

            _context.Notifies.Add(notify);
            user.Notifies.Add(notify);
            await _context.SaveChangesAsync();

            return notify;
        }

        public async Task<User> GetUserById(int id)
        {
            var dbUser = await _context.Users
                            .Include(u => u.Subscribers)
                            .Include(u => u.Friends)
                            .Include(u => u.Notifies)
                            .FirstOrDefaultAsync(u => u.Id == id);

            return dbUser;
        }

        public async Task Sub(User me, User friend)
        {
            friend.Subscribers.Add(me);
            await _context.SaveChangesAsync();
        }

        public async Task AddFriend(User me, User sub){
            me.Subscribers.Remove(sub);
            me.Friends.Add(sub);
            sub.Friends.Add(me);

            await SaveChangesAsync();
        }
    }
}