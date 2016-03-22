using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Messaging.Model
{
    public class MmsMessage : TextMessage
    {
        [JsonProperty("FileID")]
        public long? FileId { get; set; }

        public MmsMessage()
        {
            DeliveryMethod = DeliveryMethod.Mms;
        }
    }
}