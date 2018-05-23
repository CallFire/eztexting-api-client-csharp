using System.Collections.Generic;
using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Api.Media.Model;

namespace EzTextingApiClient.Api.Media
{
    /// <summary>
    /// API for managing media files in your account
    /// </summary>
    public class MediaLibraryApi
    {
        private const string FilesPath = "/sending/files?format=json";
        private const string FilesItemPath = "/sending/files/{}?format=json";

        private readonly RestApiClient _client;

        public MediaLibraryApi(RestApiClient client)
        {
            this._client = client;
        }

        /// <summary>
        /// Create a new mediafile that will be stored in your Ez Texting media library
        /// </summary>
        /// <param name="url">url to download file</param>
        /// <returns>created media file object</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public MediaFile Create(string url)
        {
            return _client.Post<MediaFile>(FilesPath, ClientUtils.AsParams("Source", url)).Entry;
        }
        
        /// <summary>
        /// Create a new mediafile that will be stored in your Ez Texting media library
        /// </summary>
        /// <param name="pathToFile">absolute path to file</param>
        /// <returns>created media file object</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public MediaFile Upload(string pathToFile)
        {
            return _client.PostFile<MediaFile>(FilesPath, pathToFile).Entry;
        }

        /// <summary>
        /// Get a single file stored in your Ez Texting media library.
        /// </summary>
        /// <param name="id">file's id</param>
        /// <returns>media file</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public MediaFile Get(long id)
        {
            return _client.Get<MediaFile>(FilesItemPath.ReplaceFirst(ClientConstants.Placeholder, id.ToString())).Entry;
        }

        /// <summary>
        /// Get list of media files stored in your Ez Texting media library.
        /// </summary>
        /// <param name="request">request object with sorting and pagination options</param>
        /// <returns>multiple media files</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public IList<MediaFile> Get(GetRequest request)
        {
            return _client.Get<MediaFile>(FilesPath, request).Entries;
        }

        /// <summary>
        /// Delete file in your Ez Texting media library
        /// </summary>
        /// <param name="id">file's id</param>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public void Delete(long id)
        {
            _client.Delete(FilesItemPath.ReplaceFirst(ClientConstants.Placeholder, id.ToString()));
        }
    }
}