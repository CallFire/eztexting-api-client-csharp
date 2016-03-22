using System;

namespace EzTextingApiClient
{
    /// <summary>
    /// Exception thrown in case error has occurred in client.
    /// </summary>
    public class EzTextingClientException : Exception
    {
        public EzTextingClientException(string message) : base(message)
        {
        }

        public EzTextingClientException(string message, Exception e) : base(message, e)
        {
        }
    }
}