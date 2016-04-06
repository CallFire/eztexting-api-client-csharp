using System.Collections.Generic;
using EzTextingApiClient.Api.Groups.Model;

namespace EzTextingApiClient.Api.Groups
{
    /// <summary>
    /// API for managing contact groups inside your account
    /// </summary>
    public class GroupsApi
    {
        private const string GroupsPath = "/groups?format=json";
        private const string GroupsItemPath = "/groups/{}?format=json";

        private readonly RestApiClient _client;

        public GroupsApi(RestApiClient client)
        {
            this._client = client;
        }

        /// <summary>
        /// Create a new group that will be stored in your Ez Texting account
        /// </summary>
        /// <param name="group">group to create</param>
        /// <returns>created group</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public Group Create(Group group)
        {
            return _client.Post<Group>(GroupsPath, group).Entry;
        }

        /// <summary>
        /// Update a group that is stored in your Ez Texting account
        /// </summary>
        /// <param name="group">group to update</param>
        /// <returns>updated group</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public Group Update(Group group)
        {
            Validate.NotNull(group.Id, "id cannot be null");
            var path = GroupsItemPath.ReplaceFirst(ClientConstants.Placeholder, group.Id.ToString());
            return _client.Post<Group>(path, group).Entry;
        }

        /// <summary>
        /// Get a single group stored in your Ez Texting account.
        /// </summary>
        /// <param name="id">group's id</param>
        /// <returns>groups that were found</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public Group Get(long id)
        {
            Validate.NotNull(id, "id cannot be blank");
            return _client.Get<Group>(GroupsItemPath.ReplaceFirst(ClientConstants.Placeholder, id.ToString())).Entry;
        }

        /// <summary>
        /// Get a list of groups stored in your Ez Texting account.
        /// </summary>
        /// <param name="request">groups that were found</param>
        /// <returns>groups that were found</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public IList<Group> Get(GetGroupsRequest request)
        {
            return _client.Get<Group>(GroupsPath, request).Entries;
        }

        /// <summary>
        /// Delete a group that is stored in your Ez Texting account
        /// </summary>
        /// <param name="id">group's id</param>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public void Delete(long id)
        {
            Validate.NotNull(id, "id cannot be blank");
            _client.Delete(GroupsItemPath.ReplaceFirst(ClientConstants.Placeholder, id.ToString()));
        }
    }
}