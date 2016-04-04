using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Messaging.Model
{
    public class VoiceMessage : AbstractMessage
    {
        [JsonProperty("CallerPhonenumber")]
        public string CallerPhoneNumber { get; set; }

        public string Name { get; set; }
        public string VoiceFile { get; set; }
        public string VoiceSource { get; set; }
    }
}