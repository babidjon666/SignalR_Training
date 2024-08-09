namespace backend.DTOModel.MessageDTO
{
    public class MessageDTO
    {
        public int ChatId { get; set; }
        public string Sender { get; set; }
        public string Text { get; set; }
        public DateTime Time{ get; set; }
    }
}