using EzTextingApiClient.Api.Toolbox.Model;

namespace EzTextingApiClient.Api.Toolbox
{
    /// <summary>
    /// Helper APIs
    /// </summary>
    public class ToolboxApi
    {
        private const string CarrierLookupPath = "/sending/phone-numbers/{}?format=json";

        private readonly RestApiClient _client;

        public ToolboxApi(RestApiClient client)
        {
            this._client = client;
        }

        /// <summary>
        /// Get the wireless carrier of a valid mobile phone number (US and Canada)
        /// </summary>
        /// <param name="phoneNumber">phone number</param>
        /// <returns>the wireless carrier of a valid mobile phone number (US and Canada)</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public CarrierLookupResponse CarrierLookup(string phoneNumber)
        {
            Validate.NotBlank(phoneNumber, "phoneNumber cannot be blank");
            var queryParams = ClientUtils.AsParams("PhoneNumber", phoneNumber);
            var path = CarrierLookupPath.ReplaceFirst(ClientConstants.Placeholder, phoneNumber);
            return _client.Get<CarrierLookupResponse>(path, queryParams).Entry;
        }
    }
}