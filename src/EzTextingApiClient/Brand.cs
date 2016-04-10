using System.Runtime.Serialization;

namespace EzTextingApiClient
{
    public enum Brand
    {
        [EnumMember(Value = ClientConstants.EzBasePathProperty)] Ez,
        [EnumMember(Value = ClientConstants.CtBasePathProperty)] Ct,
        [EnumMember(Value = ClientConstants.GtBasePathProperty)] Gt,
        [EnumMember(Value = ClientConstants.TmcBasePathProperty)] Tmc
    }
}