using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Messaging.Model
{
    public class VoiceMessage : AbstractMessage
    {
        [JsonProperty("CallerPhonenumber")]
        public string CallerPhoneNumber { get; }

        public string Name { get; }
        public string VoiceFile { get; }
        public string VoiceSource { get; }
    }
}