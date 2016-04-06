using System;
using EzTextingApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Contacts.Model
{
    public class GetContactsRequest : GetRequest
    {
        [JsonProperty("query")]
        public QueryProperty? Query { get; set; }

        [JsonProperty("source")]
        public SourceType? Source { get; set; }

        [JsonProperty("sortBy")]
        public SortProperty? SortBy { get; set; }

        [JsonProperty("optout")]
        public bool OptOut { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }
    }
}