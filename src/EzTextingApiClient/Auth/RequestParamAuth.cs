using RestSharp.Extensions;

namespace EzTextingApiClient.Auth
{
    /// Implementation of EzTexting auth scheme
    public class RequestParamAuth : IAuthentication
    {
        private readonly string _authParams;
        private readonly string _userName;
        private readonly string _password;

        /// Constructs EzTexting auth from provided credentials
        ///
        /// <param name="username">username api username</param>
        /// <param name="password">password api password</param>
        public RequestParamAuth(string username, string password)
        {
            _userName = username;
            _password = password;
            _authParams = "User" + "=" + username.UrlEncode() + "&" + "Password" + "=" + password.UrlEncode();
        }

        public string AsParamString()
        {
            return _authParams;
        }
        
        public string GetUsername()
        {
            return _userName;
        }

        public string GetPassword()
        {
            return _password;
        }
    }
}