using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Common.Model
{
    public abstract class GetRequest : EzTextingModel
    {
        [JsonProperty("sortDir")]
        public SortType SortType { get; set; }

        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
    }
}