using EzTextingApiClient.Api.Common.Model;

namespace EzTextingApiClient.Api.Keywords.Model
{
    public class CheckAvailabilityResponse : EzTextingModel
    {
        public string Keyword { get; private set; }
        public bool? Available { get; private set; }

        public CheckAvailabilityResponse()
        {
        }

        public CheckAvailabilityResponse(string keyword, bool available)
        {
            Keyword = keyword;
            Available = available;
        }

        public override string ToString() =>
            $"Keyword: {Keyword}, Available: {Available}";
    }
}