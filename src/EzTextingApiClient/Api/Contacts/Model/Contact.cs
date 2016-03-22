using System;
using System.Collections.Generic;
using EzTextingApiClient.Api.Common.Model;
using Newtonsoft.Json;

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
        public SourceType? Source { get; }

        [QueryParamAsNumber]
        public bool? OptOut { get; set; }

        public IList<string> Groups { get; set; }

//        [JsonProperty(pattern = "MM-dd-yyyy")]
        public DateTime? CreatedAt { get; }
    }
}