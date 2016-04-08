using EzTextingApiClient.Api.Credits.Model;

namespace EzTextingApiClient.Api.Credits
{
    /// <summary>
    /// API for managing credits in your account
    /// </summary>
    public class CreditsApi
    {
        private const string CheckBalancePath = "/billing/credits/get?format=json";
        private const string BuyCreditsPath = "/billing/credits?format=json";

        private readonly RestApiClient _client;

        public CreditsApi(RestApiClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Checks credit balances on your account.
        /// </summary>
        /// <returns>account balance</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public CreditBalance CheckBalance()
        {
            return _client.Get<CreditBalance>(CheckBalancePath).Entry;
        }

        /// <summary>
        /// Buys more credits for your account. You may purchase credits using a credit card you have stored in your
        /// Ez Texting account, or you may pass credit card details when you call the API.
        /// </summary>
        /// <param name="request">request object</param>
        /// <returns>amount spent and account balance</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public BuyCreditsResponse BuyCredits(BuyCreditsRequest request)
        {
            return _client.Post<BuyCreditsResponse>(BuyCreditsPath, request).Entry;
        }
    }
}