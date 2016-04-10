using EzTextingApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace EzTextingApiClient.Api.Toolbox.Model
{
    public class CarrierLookupResponse : EzTextingModel
    {
        public string PhoneNumber { get; private set; }

        [JsonConverter(typeof(CarrierNameConverter))]
        public CarrierName CarrierName { get; private set; }

        public override string ToString() =>
            $"CarrierLookupResponse [PhoneNumber={PhoneNumber}, CarrierName={CarrierName}]";
    }
}