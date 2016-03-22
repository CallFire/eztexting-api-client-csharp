using System.Runtime.Serialization;

namespace EzTextingApiClient.Api.Contacts.Model
{
    public enum SourceType
    {
        [EnumMember(Value = "Unknown")] Unknown,
        [EnumMember(Value = "Manually Added")] Manual,
        [EnumMember(Value = "Upload")] Upload,
        [EnumMember(Value = "Web Widget")] WebWidget,
        [EnumMember(Value = "API")] Api,
        [EnumMember(Value = "Keyword")] Keyword
    }
}