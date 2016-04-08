using EzTextingApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Inbox.Model
{
    public class GetMessagesRequest : GetRequest
    {
        [JsonProperty("FolderID")]
        public long? FolderId { get; set; }

        public string Search { get; set; }

        [JsonProperty("Filter")]
        public MessageType Type { get; set; }

        [JsonProperty("sortBy")]
        public SortProperty? SortBy { get; set; }
    }
}