using System.Collections.Generic;
using EzTextingApiClient.Api.Inbox.Model;

namespace EzTextingApiClient.Api.Inbox
{
    /// <summary>
    /// API for managing your Inbox
    /// </summary>
    public class InboxApi
    {
        private const string FoldersPath = "/messages-folders?format=json";
        private const string FoldersItemPath = "/messages-folders/{}?format=json";
        private const string MessagesPath = "/incoming-messages?format=json";
        private const string MessagesItemPath = "/incoming-messages/{}?format=json";
        private const string MoveMessagePath = "/incoming-messages/?format=json&_method=move-to-folder";

        private readonly RestApiClient _client;

        public InboxApi(RestApiClient client)
        {
            this._client = client;
        }

        /// <summary>
        /// Get incoming text message in your Ez Texting Inbox
        /// </summary>
        /// <param name="id">message's id</param>
        /// <returns>inbox message</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public InboxMessage GetMessage(long id)
        {
            var path = MessagesItemPath.ReplaceFirst(ClientConstants.Placeholder, id.ToString());
            return _client.Get<InboxMessage>(path).Entry;
        }

        /// <summary>
        /// Get all incoming text messages in your Ez Texting Inbox
        /// </summary>
        /// <param name="request">request with filtering fields</param>
        /// <returns>list with messages</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public IList<InboxMessage> GetMessages(GetMessagesRequest request)
        {
            return _client.Get<InboxMessage>(MessagesPath, request).Entries;
        }

        /// <summary>
        /// Moves an incoming text message in your Ez Texting Inbox to a specified folder.
        /// </summary>
        /// <param name="id">message's id</param>
        /// <param name="folderId">destination folder's id</param>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public void MoveMessage(long id, long folderId)
        {
            _client.Post<object>(MoveMessagePath, ClientUtils.AsParams("ID", id, "FolderID", folderId));
        }

        /// <summary>
        /// Moves an incoming text messages in your Ez Texting Inbox to a specified folder.
        /// </summary>
        /// <param name="ids">list with message ids</param>
        /// <param name="folderId">destination folder's id</param>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public void MoveMessages(IList<long> ids, long folderId)
        {
            Validate.NotNull(ids, "ids");
            _client.Post<object>(MoveMessagePath, ClientUtils.AsParams("ID", ids, "FolderID", folderId));
        }

        /// <summary>
        /// Delete an incoming text message in your Ez Texting Inbox
        /// </summary>
        /// <param name="id">message's id</param>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public void DeleteMessage(long id)
        {
            _client.Delete(MessagesItemPath.ReplaceFirst(ClientConstants.Placeholder, id.ToString()));
        }

        /// <summary>
        /// Create a new folder that will be stored in your Ez Texting account
        /// </summary>
        /// <param name="name">the name of the folder</param>
        /// <returns>created folder</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public Folder CreateFolder(string name)
        {
            Validate.NotBlank(name, "name cannot be blank");
            var queryParams = ClientUtils.AsParams("Name", name);
            var folder = _client.Post<Folder>(FoldersPath, queryParams).Entry;
            // API returns only id, so setting name
            folder.Name = name;
            return folder;
        }

        /// <summary>
        /// Update a folder that is stored in your Ez Texting account
        /// </summary>
        /// <param name="folder">folder to update</param>
        /// <returns>updated folder</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public void UpdateFolder(Folder folder)
        {
            Validate.NotNull(folder.Id, "folder.id");
            var path = FoldersItemPath.ReplaceFirst(ClientConstants.Placeholder, folder.Id.ToString());
            _client.Post<object>(path, folder);
        }

        /// <summary>
        /// Get a single folder stored in your Ez Texting account.
        /// </summary>
        /// <param name="id">folder's id</param>
        /// <returns>folders that were found</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public Folder GetFolder(long id)
        {
            return _client.Get<Folder>(FoldersItemPath.ReplaceFirst(ClientConstants.Placeholder, id.ToString())).Entry;
        }

        /// <summary>
        /// Get a list of folders stored in your Ez Texting account.
        /// </summary>
        /// <returns>all account's folders</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public IList<Folder> GetFolders()
        {
            return _client.Get<Folder>(FoldersPath).Entries;
        }

        /// <summary>
        /// Delete a folder that is stored in your Ez Texting account
        /// </summary>
        /// <param name="id">folder's id</param>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public void DeleteFolder(long id)
        {
            _client.Delete(FoldersItemPath.ReplaceFirst(ClientConstants.Placeholder, id.ToString()));
        }
    }
}