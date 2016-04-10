using System;
using System.Collections;
using System.Collections.Generic;
using EzTextingApiClient.Api.Messaging.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EzTextingApiClient.Api.Inbox.Model
{
    /// Message stored in your inbox folder
    public class InboxMessage : SimpleMessage
    {
        [JsonProperty("ID")]
        public long? Id { get; private set; }

        public string PhoneNumber { get; private set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageType? Type { get; private set; }
        public IList<string> Files { get; private set; }

        [JsonProperty("New")]
        public bool? Unread { get; private set; }

        [JsonProperty("FolderID")]
        public long? FolderId { get; private set; }

        [JsonProperty("ContactID")]
        public string ContactId { get; private set; }

        public DateTime? ReceivedOn { get; private set; }

        public override string ToString() =>
            $"InboxMessage [{base.ToString()} Id={Id}, PhoneNumber={PhoneNumber}, Type={Type}, Unread={Unread} " +
            $"Files={",".Join(Files)}, FolderId={FolderId}, ContactId={ContactId}, ReceivedOn={ReceivedOn}";
    }
}