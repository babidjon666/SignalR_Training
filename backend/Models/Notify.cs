using backend.Enums;

namespace backend.Models
{
    public class Notify
    {
        public int Id { get; set; }
        public NotifyType NotifyType { get; set; }
        public string Text { get; set; }
        public bool IsReaded { get; set; }
        public DateTime Time { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    } 
}