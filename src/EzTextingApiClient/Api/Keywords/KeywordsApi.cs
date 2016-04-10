using System.Collections.Generic;
using EzTextingApiClient.Api.Credits.Model;
using EzTextingApiClient.Api.Keywords.Model;

namespace EzTextingApiClient.Api.Keywords
{
    /// <summary>
    /// API for managing keywords: check availability, rent, configure, cancel
    /// </summary>
    public class KeywordsApi
    {
        private const string KeywordsPath = "/keywords?format=json";
        private const string KeywordsItemPath = "/keywords/{}?format=json";
        private const string CheckAvailabilityPath = "/keywords/new?Keyword={}&format=json";

        private readonly RestApiClient _client;

        public KeywordsApi(RestApiClient client)
        {
            this._client = client;
        }

        /// <summary>
        /// Check whether a Keyword is available to rent on Ez Texting's short code. Please note, we will check
        /// availability for the country your account is set to.
        /// </summary>
        /// <param name="keyword">keyword to check</param>
        /// <returns>true if keyword is available to rent, otherwise false returned</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public bool CheckAvailability(string keyword)
        {
            Validate.NotBlank(keyword, "keyword cannot be blank");
            // TODO ugly code, have to wait api fixes
            try
            {
                var queryParams = ClientUtils.AsParams("Keyword", keyword);
                var path = CheckAvailabilityPath.ReplaceFirst(ClientConstants.Placeholder, keyword);
                var avail = _client.Get<CheckAvailabilityResponse>(path, queryParams).Entry.Available;
                return avail != null && avail.Value;
            }
            catch (EzTextingApiException e)
            {
                if (e.Errors != null && e.Errors.Contains($"Keyword: The keyword '{keyword}' is unavailable"))
                    return false;
                throw;
            }
        }

        /// <summary>
        /// Rents a Keyword for use on Ez Texting's short code in the country your account is set to send messages to.
        /// You may rent a Keyword using a credit card you have stored in your Ez Texting account, or you may pass credit
        /// card details when you call the API.
        /// </summary>
        /// <param name="keyword">keyword for rent</param>
        /// <param name="ccNumber">ast four digits of any card stored in your Ez Texting account.</param>
        /// <returns>keyword</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public Keyword Rent(string keyword, string ccNumber)
        {
            Validate.NotBlank(keyword, "keyword cannot be blank");
            Validate.NotBlank(ccNumber, "ccNumber cannot be blank");
            var queryParams = ClientUtils.AsParams("Keyword", keyword, "StoredCreditCard", ccNumber);
            return _client.Post<Keyword>(KeywordsPath, queryParams).Entry;
        }

        /// <summary>
        /// Rents a Keyword for use on Ez Texting's short code in the country your account is set to send messages to.
        /// You may rent a Keyword using a credit card you have stored in your Ez Texting account, or you may pass credit
        /// card details when you call the API.
        /// </summary>
        /// <param name="keyword">keyword for rent</param>
        /// <param name="creditCard">credit card for payment</param>
        /// <returns>keyword</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public Keyword Rent(string keyword, CreditCard creditCard)
        {
            Validate.NotBlank(keyword, "keyword cannot be blank");
            return _client.Post<Keyword>(KeywordsPath, creditCard, ClientUtils.AsParams("Keyword", keyword)).Entry;
        }

        /// <summary>
        /// Get a list of groups stored in your Ez Texting account.
        /// </summary>
        /// <param name="keyword">keyword to configure</param>
        /// <returns>updated keyword</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public Keyword Setup(Keyword keyword)
        {
            Validate.NotBlank(keyword.Value, "keyword cannot be blank");
            var path = KeywordsItemPath.ReplaceFirst(ClientConstants.Placeholder, keyword.Value);
            return _client.Post<Keyword>(path, keyword).Entry;
        }

        /// <summary>
        /// Cancels an active Keyword on Ez Texting's short code in the country your account is set to send messages to.
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public void Cancel(string keyword)
        {
            Validate.NotBlank(keyword, "keyword cannot be blank");
            _client.Delete(KeywordsItemPath.ReplaceFirst(ClientConstants.Placeholder, keyword));
        }
    }
}