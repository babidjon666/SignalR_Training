using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Chat
    {
        public int Id { get; set; }
        [JsonIgnore]
        public List<User> Users { get; set; } = new List<User>();
        [JsonIgnore]
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}