using EzTextingApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Groups.Model
{
    public class Group : EzTextingModel
    {
        [JsonProperty("ID")]
        public long? Id { get; set; }

        public string Name { get; set; }
        public string Note { get; set; }
        public long? ContactCount { get; private set; }

        public override string ToString() =>
            $"Group [Id={Id}, Name={Name}, Note={Note}, ContactCount={ContactCount}]";
    }
}