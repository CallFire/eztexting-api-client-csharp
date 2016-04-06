using System;
using System.Collections.Generic;
using EzTextingApiClient.Api.Common.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EzTextingApiClient.Api.Contacts.Model
{
    public class Contact : EzTextingModel
    {
        [JsonProperty("ID")]
        public string Id { get; set; }

        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SourceType? Source { get; private set; }

        [QueryParamAsNumber]
        public bool? OptOut { get; set; }

        public IList<string> Groups { get; set; }

        [JsonConverter(typeof(CreatedAtConvertor))]
        public DateTime? CreatedAt { get; private set; }

        public override string ToString() =>
            $"Contact [Id={Id}, PhoneNumber={PhoneNumber}, FirstName={FirstName}, LastName={LastName}, Email={Email}, " +
            $"Note={Note}, Source={Source}, OptOut={OptOut}, Groups={",".Join(Groups)} CreatedAt={CreatedAt}]";
    }
}