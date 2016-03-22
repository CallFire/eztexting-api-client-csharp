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
        public DateTime? StampToSend;
        public IList<string> Groups { get; }
        public IList<string> PhoneNumbers { get; }
        public IList<string> LocalOptOuts { get; }
        public IList<string> GlobalOptOuts { get; }
    }
}