using System.Runtime.Serialization;

namespace EzTextingApiClient.Api.Inbox.Model
{
    public enum MessageType
    {
        [EnumMember(Value = "SMS")] Sms,
        [EnumMember(Value = "MMS")] Mms
    }
}