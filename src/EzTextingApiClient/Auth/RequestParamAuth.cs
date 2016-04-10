using RestSharp.Extensions;

namespace EzTextingApiClient.Auth
{
    /// Implementation of EzTexting auth scheme
    public class RequestParamAuth : IAuthentication
    {
        private readonly string _authParams;

        /// Constructs EzTexting auth from provided credentials
        ///
        /// <param name="username">username api username</param>
        /// <param name="password">password api password</param>
        public RequestParamAuth(string username, string password)
        {
            _authParams = "User" + "=" + username.UrlEncode() + "&" + "Password" + "=" + password.UrlEncode();
        }

        public string AsParamString()
        {
            return _authParams;
        }
    }
}