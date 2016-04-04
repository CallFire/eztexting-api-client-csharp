using System;
using System.Collections.Generic;

namespace EzTextingApiClient
{
    /// <summary>
    /// EzTexting API exception is thrown by client in case of 4xx or 5xx HTTP code response
    /// </summary>
    public class EzTextingApiException : Exception
    {
        /// Detailed error description
        public IList<string> Errors { get; set; }

        /// Server response code
        public int HttpStatusCode { get; set; }

        public EzTextingApiException(int httpStatusCode, IList<string> errors)
        {
            HttpStatusCode = httpStatusCode;
            Errors = errors;
        }

        public override string Message => Errors.ToPrettyString();

        public override string ToString()
        {
            return $"[EzTextingApiException: Errors={Errors?.ToPrettyString()}, HttpStatusCode={HttpStatusCode}]";
        }
    }
}