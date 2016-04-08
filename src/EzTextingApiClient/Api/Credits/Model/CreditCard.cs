using EzTextingApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Credits.Model
{
    public class CreditCard : EzTextingModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public string Country { get; set; }

        [JsonProperty("CreditCardTypeID")]
        public CreditCardType? Type { get; set; }

        public string Number { get; set; }
        public string SecurityCode { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }

        public override string ToString() =>
            $"CreditCard [FirstName={FirstName}, LastName={LastName}, Street={Street}, City={City}, State={State}, " +
            $"Zip={Zip}, Country={Country}, Type={Type}, Number={Number}, SecurityCode={SecurityCode}, " +
            $"ExpirationMonth={ExpirationMonth}, ExpirationYear={ExpirationYear}]";
    }
}