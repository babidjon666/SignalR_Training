using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notify> Notifies { get; set; } // Добавлено

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Пользователь и Чат имеют связь многие ко многим
            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(c => c.Users)
                .UsingEntity(j => j.ToTable("UserChats")); // Имя промежуточной таблицы

            // Сообщение и Чат имеют связь многие к одному
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            // Сообщение и Пользователь имеют связь многие к одному
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Owner)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Пользователи и их друзья (многие ко многим)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "UserFriends",
                    j => j.HasOne<User>().WithMany().OnDelete(DeleteBehavior.NoAction),
                    j => j.HasOne<User>().WithMany().OnDelete(DeleteBehavior.NoAction));

            // Пользователи и их подписчики (многие ко многим)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Subscribers)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "UserSubscribers",
                    j => j.HasOne<User>().WithMany().OnDelete(DeleteBehavior.NoAction),
                    j => j.HasOne<User>().WithMany().OnDelete(DeleteBehavior.NoAction));

            // Пользователь и уведомления (один ко многим
            
             modelBuilder.Entity<Notify>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifies)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}