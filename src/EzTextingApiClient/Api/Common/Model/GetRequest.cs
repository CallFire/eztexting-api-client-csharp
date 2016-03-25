using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Common.Model
{
    public abstract class GetRequest : EzTextingModel
    {
        [JsonProperty("sortDir")]
        public SortType SortType { get; set; }

        [JsonProperty("itemsPerPage")]
        public int ItemsPerPage { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }
    }
}