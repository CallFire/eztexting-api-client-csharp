using EzTextingApiClient.Api.Common.Model;

namespace EzTextingApiClient.Api.Credits.Model
{
    public class CreditBalance : EzTextingModel
    {
        public long? PlanCredits { get; private set; }
        public long? AnytimeCredits { get; private set; }
        public long? TotalCredits { get; private set; }

        public override string ToString() =>
            $"CreditBalance [PlanCredits={PlanCredits}, AnytimeCredits={AnytimeCredits}, TotalCredits={TotalCredits}]";
    }
}