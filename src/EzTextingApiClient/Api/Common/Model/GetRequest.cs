using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Common.Model
{
    public class GetRequest : EzTextingModel
    {
        [JsonProperty("sortDir")]
        public SortType SortType { get; set; }

        [JsonProperty("itemsPerPage")]
        public int ItemsPerPage { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        public override string ToString() =>
            $"GetRequest [SortType={SortType}, ItemsPerPage={ItemsPerPage}, Page={Page}]";
    }
}