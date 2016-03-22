using System;
using System.Collections.Generic;
using EzTextingApiClient.Api.Messaging.Model;

namespace EzTextingApiClient.Api.Messaging
{
    /// <summary>
    /// Represents APIs for sending SMS/MMS messages, get short and detailed delivery reports
    /// </summary>
    public class MessagingApi
    {
        private const string SendTextPath = "/sending/messages?format=json";
        private const string SendVoicePath = "/voice/messages?format=json";
        private const string ReportPath = "/sending/reports/{}/?format=json";
        private const string DetailedReportPath = "/sending/reports/{}/view-details?format=json";

        private readonly RestApiClient _client;

        public MessagingApi(RestApiClient client)
        {
            this._client = client;
        }

        ///
        /// Sends SMS/MMS messages via the short code 313131 (393939 In Canada) to a single phone number or an array of phone numbers.
        ///
        /// <param name="message">message to send</param>
        /// <typeparam name="T">message type</typeparam>
        /// <returns>created message with additional info</returns>
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public T Send<T>(T message) where T : TextMessage
        {
            Validate.NotNull(message.DeliveryMethod, "Delivery method must be set");
            if (message is SmsMessage)
            {
                return _client.Post<SmsMessage>(SendTextPath, message).Entry as T;
            }
            else if (message is MmsMessage)
            {
                return _client.Post<MmsMessage>(SendTextPath, message).Entry as T;
            }
            else
            {
                throw new InvalidOperationException("Message of type " + message.GetType() + " isn't supported");
            }
        }

        ///
        /// Sends voice broadcast messages to an array of phone numbers or a Group in your Ez Texting account. You
        /// can use a file stored in your Ez Texting account as the source, or include the URL of a compatible file
        /// in the request.
        ///
        /// @param message voice message request
        /// @return response object
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public VoiceMessage Send(VoiceMessage message)
        {
            return _client.Post<VoiceMessage>(SendVoicePath, message).Entry;
        }

        ///
        /// Get a report for specific delivery status of a message you have sent.
        ///
        /// @param id message id
        /// @return delivery report
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public DeliveryReport GetReport(long id)
        {
            var path = ReportPath.ReplaceFirst(ClientConstants.Placeholder, id.ToString());
            return _client.Get<DeliveryReport>(path).Entry;
        }

        ///
        /// Get a report for specific delivery status of a message you have sent.
        ///
        /// @param id     message id
        /// @param status delivery status to sort by
        /// @return list of possible phone numbers which have given status
        /// <exception cref="EzTextingApiException">in case error has occurred on server side, check provided error description.</exception>
        /// <exception cref="EzTextingClientException">in case error has occurred in client.</exception>
        public IList<long> GetDetailedReport(long id, DeliveryStatus status)
        {
            var path = DetailedReportPath.ReplaceFirst(ClientConstants.Placeholder, id.ToString());
            var queryParams = ClientUtils.AsParams("status", status.ToString());
            return _client.Get<long>(path, queryParams).Entries;
        }
    }
}