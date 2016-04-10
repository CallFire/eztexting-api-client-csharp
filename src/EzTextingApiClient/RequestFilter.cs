using System;
using RestSharp;

namespace EzTextingApiClient
{
    /// <summary>
    /// Extend abstract filter in case you need to modify outgoing http requests
    /// </summary>
    public abstract class RequestFilter : IComparable<RequestFilter>
    {
        /// <summary>
        /// Default order number
        /// </summary>
        public static readonly int DefaultOrder = 10;

        /// <summary>
        /// Configure RestSharp request as you need
        /// </summary>
        /// <param name="restRequest">HTTP request build</param>
        public abstract void Filter(IRestRequest restRequest);

        /// <summary>
        /// Filters with greater order number are executed first
        /// </summary>
        /// <returns>order number</returns>
        protected int Order()
        {
            return DefaultOrder;
        }

        public int CompareTo(RequestFilter other)
        {
            return other.Order().CompareTo(Order());
        }
    }
}