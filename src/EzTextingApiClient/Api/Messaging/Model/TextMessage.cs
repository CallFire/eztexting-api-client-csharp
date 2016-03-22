using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Messaging.Model
{
    public class TextMessage : AbstractMessage
    {
        public string Subject { get; set; }
        public string Message { get; set; }

        [JsonProperty("MessageTypeID")]
        public DeliveryMethod DeliveryMethod { get; set; }

        public override string ToString()
        {
            return $"TextMessage [Subject={Subject}, Message={Message}, DeliveryMethod={DeliveryMethod}]";
        }
    }
}