using Newtonsoft.Json;

namespace FormsChatApp.Models
{
    public class Message
    {
        [JsonProperty("_id")]
        public string ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
		[JsonProperty("message")]
        public string MessageContent { get; set; }

        public Message(string name, string messageContent)
        {
            Name = name;
            MessageContent = messageContent;
        }

        [JsonConstructor]
        public Message(string id, string name, string messageContent)
        {
            ID = id;
            Name = name;
            MessageContent = messageContent;
        }

		public override string ToString()
		{
            return ID + " _ " + Name + ": " + MessageContent;
		}
	}
}
