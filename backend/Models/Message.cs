using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        [JsonIgnore]
        public Chat? Chat { get; set; }
        public int OwnerId { get; set; }
        [JsonIgnore]
        public User? Owner { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string Text { get; set; } = string.Empty;
    }
}