using EzTextingApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Inbox.Model
{
    public class Folder : EzTextingModel
    {
        [QueryParamIgnore]
        [JsonProperty("ID")]
        public long? Id { get; set; }

        public string Name { get; set; }

        public override string ToString() =>
            $"Folder [Id={Id}, Name={Name}]";
    }
}