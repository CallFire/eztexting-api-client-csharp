using EzTextingApiClient.Api.Common.Model;

namespace EzTextingApiClient.Api.Messaging.Model
{
    public class ReportItem : EzTextingModel
    {
        public long Count { get; private set; }
        public string Percentage { get; private set; }
    }
}