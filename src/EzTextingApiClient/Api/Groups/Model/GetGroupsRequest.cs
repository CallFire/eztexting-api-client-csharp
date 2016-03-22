using EzTextingApiClient.Api.Common.Model;

namespace EzTextingApiClient.Api.Groups.Model
{
    public class GetGroupsRequest : GetRequest
    {
        public SortProperty SortBy { get; set; }
    }
}