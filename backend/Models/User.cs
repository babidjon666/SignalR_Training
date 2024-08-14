using System.Text.Json.Serialization;

namespace backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserSurname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Chat> Chats{ get; set; } = new List<Chat>();
        [JsonIgnore]
        public List<Message> Messages{ get; set; } = new List<Message>();
        [JsonIgnore]
        public List<User> Friends{ get; set; } = new List<User>();
        [JsonIgnore]
        public List<User> Subscribers{ get; set; } = new List<User>();
        [JsonIgnore]
        public List<Notify> Notifies{ get; set; } = new List<Notify>();
    }
}