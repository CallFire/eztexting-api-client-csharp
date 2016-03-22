using System.Collections.Generic;

namespace EzTextingApiClient
{
    /// <summary>
    /// Exception is used by Callfire model validation methods
    /// </summary>
    public class ModelValidationException : EzTextingApiException
    {
        public ModelValidationException(string errorMessage) : base(400, new List<string>() {errorMessage})
        {
        }
    }
}