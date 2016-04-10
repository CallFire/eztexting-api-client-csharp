using EzTextingApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Messaging.Model
{
    public class DeliveryReport : EzTextingModel
    {
        public ReportItem Delivered { get; private set; }
        public ReportItem Bounced { get; private set; }

        [JsonProperty("Not Available")]
        public ReportItem NotAvailable { get; private set; }

        [JsonProperty("Not Sent - No Credits")]
        public ReportItem NoCredits { get; private set; }

        [JsonProperty("Not Sent - Opted Out")]
        public ReportItem OptedOut { get; private set; }

        public override string ToString() =>
            $"DeliveryReport[Delivered={Delivered}, Bounced={Bounced}, NotAvailable={NotAvailable}, NoCredits={NoCredits}, OptedOut={OptedOut}]";
    }
}