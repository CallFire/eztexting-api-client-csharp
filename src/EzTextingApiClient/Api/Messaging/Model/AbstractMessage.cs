using System;
using System.Collections.Generic;
using EzTextingApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Messaging.Model
{
    public class AbstractMessage : EzTextingModel
    {
        [JsonProperty("ID")] public long? Id;
        public long? RecipientsCount;
        public long? Credits;
        public DateTime? StampToSend { get; set; }
        public IList<string> Groups { get; set; }
        public IList<string> PhoneNumbers { get; set; }
        public IList<string> LocalOptOuts { get; set; }
        public IList<string> GlobalOptOuts { get; set; }
    }
}