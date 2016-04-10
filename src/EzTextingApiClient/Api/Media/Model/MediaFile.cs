using EzTextingApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Media.Model
{
    public class MediaFile : EzTextingModel
    {
        [JsonProperty("ID")]
        public long? Id { get; private set; }

        public string Name { get; private set; }
        public string Path { get; private set; }

        public override string ToString() =>
            $"MediaFile [Id={Id}, Name={Name}, Path={Path}]";
    }
}