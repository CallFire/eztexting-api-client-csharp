namespace EzTextingApiClient.Api.Credits.Model
{
    public class BuyCreditsResponse : CreditBalance
    {
        public long? BoughtCredits { get; private set; }
        public decimal? Amount { get; private set; }
        public decimal? Discount { get; private set; }

        public override string ToString() =>
            $"BuyCreditsResponse [{base.ToString()}, BoughtCredits={BoughtCredits}, Amount={Amount}, Discount={Discount}]";
    }
}