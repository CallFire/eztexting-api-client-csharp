namespace EzTextingApiClient.Auth
{
    public interface IAuthentication
    {
        string AsParamString();
        string GetUsername();
        string GetPassword();
    }
}