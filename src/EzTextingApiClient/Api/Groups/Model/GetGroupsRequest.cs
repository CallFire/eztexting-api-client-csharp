using EzTextingApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Groups.Model
{
    public class GetGroupsRequest : GetRequest
    {
        [JsonProperty("sortBy")]
        public SortProperty SortBy { get; set; }
    }
}