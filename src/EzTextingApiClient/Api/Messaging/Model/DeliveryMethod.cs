using System.Runtime.Serialization;

namespace EzTextingApiClient.Api.Messaging.Model
{
    /// Defines message delivery methods.
    /// 1 to send via Express delivery method
    /// set to 2 to send via Standard delivery method; set to 3 to send via MMS delivery method
    public enum DeliveryMethod
    {
        [EnumMember(Value = "1")] Express,
        [EnumMember(Value = "2")] Standard,
        [EnumMember(Value = "3")] Mms
    }
}