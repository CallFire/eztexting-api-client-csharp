using System.Collections.Generic;
using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Api.Messaging.Model;
using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Keywords.Model
{
    public class Keyword : EzTextingModel
    {
        [QueryParamIgnore]
        [JsonProperty("ID")]
        public long? Id { get; set; }

        [JsonProperty("Keyword")]
        public string Value { get; set; }

        public bool? EnableDoubleOptIn { get; set; }
        public bool? EnableAlternateReply { get; set; }
        public SimpleMessage ConfirmMessage { get; set; }
        public SimpleMessage JoinMessage { get; set; }
        public SimpleMessage AlternateReply { get; set; }
        public string ForwardEmail { get; set; }
        public string ForwardUrl { get; set; }

        [JsonProperty("ContactGroupIDs")]
        public IList<string> ContactGroups { get; set; }

        public override string ToString() =>
            $"Keyword [Id={Id}, Value={Value}, EnableDoubleOptIn={EnableDoubleOptIn}, EnableAlternateReply={EnableAlternateReply}, " +
            $"ConfirmMessage={ConfirmMessage}, JoinMessage={JoinMessage}, AlternateReply={AlternateReply}, " +
            $"ForwardEmail={ForwardEmail}, ForwardUrl={ForwardUrl}, ContactGroups={",".Join(ContactGroups)}";
    }
}