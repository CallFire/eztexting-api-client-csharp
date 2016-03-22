using System;
using EzTextingApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Messaging.Model
{
    /// Basic message with subject, message itself and type
    public class SimpleMessage : EzTextingModel
    {
        public string Subject { get; set; }
        public string Message { get; set; }

        [JsonProperty("MessageTypeID")]
        public DeliveryMethod DeliveryMethod { get; set; }

        public override string ToString()
        {
            return $"SimpleMessage [Subject={Subject}, Message={Message}, DeliveryMethod={DeliveryMethod}]";
        }
    }
}