using System.Runtime.Serialization;

namespace EzTextingApiClient.Api.Common.Model
{
    public enum SortType
    {
        [EnumMember(Value = "asc")] Asc,
        [EnumMember(Value = "desc")] Desc
    }
}