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
        }
    }
}