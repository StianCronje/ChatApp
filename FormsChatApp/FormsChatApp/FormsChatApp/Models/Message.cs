namespace FormsChatApp.Models
{
    public class Message
    {
        public string Name { get; set; }
        public string MessageContent { get; set; }

        public Message(string name, string messageContent)
        {
            Name = name;
            MessageContent = messageContent;
        }
    }
}
