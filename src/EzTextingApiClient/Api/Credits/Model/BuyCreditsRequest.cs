using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Credits.Model
{
    public class BuyCreditsRequest : CreditCard
    {
        [JsonProperty("NumberOfCredits")]
        public long? Credits { get; set; }

        public string CouponCode { get; set; }

        [JsonProperty("StoredCreditCard")]
        public string StoredCard { get; set; }

        public override string ToString() =>
            $"BuyCreditsRequest [{base.ToString()}, Credits={Credits}, CouponCode={CouponCode}, StoredCard={StoredCard}]";
    }
}