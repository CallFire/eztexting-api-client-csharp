using System.Runtime.Serialization;

namespace EzTextingApiClient.Api.Messaging.Model
{
    public enum DeliveryStatus
    {
        [EnumMember(Value = "na")] Na,
        [EnumMember(Value = "delivered")] Delivered,
        [EnumMember(Value = "no_credits")] NoCredits,
        [EnumMember(Value = "bounced")] Bounced,
        [EnumMember(Value = "opted_out")] OptedOut
    }
}