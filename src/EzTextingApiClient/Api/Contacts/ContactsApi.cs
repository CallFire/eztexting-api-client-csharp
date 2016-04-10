using System.Collections.Generic;
using EzTextingApiClient.Api.Contacts.Model;

namespace EzTextingApiClient.Api.Contacts
{
    /// <summary>
    /// API for managing contacts inside your account
    /// </summary>
    public class ContactsApi
    {
        private const string ContactsPath = "/contacts?format=json";
        private const string ContactsItemPath = "/contacts/{}?format=json";

        private readonly RestApiClient _client;

        public ContactsApi(RestApiClient client)
        {
            this._client = client;
        }

        /// <summary>
        /// Create a new contact that will be stored in your Ez Texting account
        /// </summary>
        /// <param name="contact">contact to create</param>
        /// <returns>created contact</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public Contact Create(Contact contact)
        {
            return _client.Post<Contact>(ContactsPath, contact).Entry;
        }

        /// <summary>
        /// Update a contact that is stored in your Ez Texting account
        /// </summary>
        /// <param name="contact">contact to update</param>
        /// <returns>updated contact</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public Contact Update(Contact contact)
        {
            Validate.NotNull(contact.Id, "id cannot be null");
            var path = ContactsItemPath.ReplaceFirst(ClientConstants.Placeholder, contact.Id);
            return _client.Post<Contact>(path, contact).Entry;
        }

        /// <summary>
        /// Get a single contact stored in your Ez Texting account.
        /// </summary>
        /// <param name="id">contact's id</param>
        /// <returns>contacts that were found</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public Contact Get(string id)
        {
            Validate.NotBlank(id, "id cannot be blank");
            return _client.Get<Contact>(ContactsItemPath.ReplaceFirst(ClientConstants.Placeholder, id)).Entry;
        }

        /// <summary>
        /// Get a list of contacts stored in your Ez Texting account.
        /// </summary>
        /// <param name="request">contacts that were found</param>
        /// <returns>contacts that were found</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public IList<Contact> Get(GetContactsRequest request)
        {
            return _client.Get<Contact>(ContactsPath, request).Entries;
        }

        /// <summary>
        /// Delete a contact that is stored in your Ez Texting account
        /// </summary>
        /// <param name="id">contact's id</param>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public void Delete(string id)
        {
            Validate.NotBlank(id, "id cannot be blank");
            _client.Delete(ContactsItemPath.ReplaceFirst(ClientConstants.Placeholder, id));
        }
    }
}