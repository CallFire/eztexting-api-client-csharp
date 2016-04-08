using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using System.Collections;
using System.Configuration;
using System.Text;
using System.IO;
using System.Net;
using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Auth;
using RestSharp.Extensions;

namespace EzTextingApiClient
{
    /// <summary>
    /// REST client which makes HTTP calls to EzTexting service
    /// </summary>
    public class RestApiClient
    {
        public Brand Brand { get; }
        public static KeyValueConfigurationCollection ClientConfig { get; }
        private readonly ISerializer _jsonSerializer;
        private readonly IDeserializer _jsonDeserializer;
        private readonly IAuthentication _authentication;
        private static readonly Logger Logger = new Logger();

        /// <summary>
        /// RestSharp client configured to query EzTexting API
        /// </summary>
        /// <returns>RestSharp client interface</returns>
        public IRestClient RestClient { get; set; }

        /// <summary>
        /// Returns base URL path for API endpoints
        /// </summary>
        /// <returns>string representation of base URL path</returns>
        public string ApiBasePath => ClientConfig[Brand.EnumMemberAttr()].Value;

        /// <summary>
        /// Returns HTTP request filters associated with API client
        /// </summary>
        /// <value>active filters.</value>
        public SortedSet<RequestFilter> Filters { get; }

        /// <summary>
        /// loads client configuration
        /// </summary>
        static RestApiClient()
        {
            ClientConfig = LoadAppSettings();
        }

        /// <summary>
        ///     REST API client constructor
        /// </summary>
        /// <param name="brand">
        ///     EzTexting brand you want to connect
        /// </param>
        /// <param name="authentication">
        ///     authentication API authentication method
        /// </param>
        public RestApiClient(Brand brand, IAuthentication authentication)
        {
            Brand = brand;
            _authentication = authentication;
            _jsonSerializer = new EzTextingJsonConverter();
            _jsonDeserializer = (IDeserializer) _jsonSerializer;

            RestClient = new RestClient(ApiBasePath)
            {
                UserAgent = GetType().Assembly.GetName().Name + "-csharp-" + GetType().Assembly.GetName().Version
            };
            RestClient.AddHandler("application/json", _jsonDeserializer);

            var proxyAddress = ClientConfig[ClientConstants.ProxyAddressProperty]?.Value;
            var proxyCredentials = ClientConfig[ClientConstants.ProxyCredentialsProperty]?.Value;

            if (!string.IsNullOrEmpty(proxyAddress))
            {
                Logger.Debug("Configuring proxy host for client: {} auth: {}", proxyAddress, proxyCredentials);
                var parsedAddress = proxyAddress.Split(':');
                var portValue = parsedAddress.Length > 1
                    ? ClientUtils.StrToIntDef(parsedAddress[1], ClientConstants.DefaultProxyPort)
                    : ClientConstants.DefaultProxyPort;
                var proxy = new WebProxy(parsedAddress[0], portValue);

                if (!string.IsNullOrEmpty(proxyCredentials))
                {
                    var parsedCredentials = proxyCredentials.Split(':');
                    if (parsedCredentials.Length > 1)
                    {
                        proxy.Credentials = new NetworkCredential(parsedCredentials[0], parsedCredentials[1]);
                    }
                    else
                    {
                        Logger.Debug("Proxy credentials have wrong format, must be username:password");
                    }
                }
                RestClient.Proxy = proxy;
            }

            Filters = new SortedSet<RequestFilter>();
        }

        /// <summary>
        /// Loads client's app settings config section
        /// </summary>
        public static KeyValueConfigurationCollection LoadAppSettings()
        {
            var path = typeof(RestApiClient).Assembly.Location;
            var config = ConfigurationManager.OpenExeConfiguration(path);
            var appSettings = (AppSettingsSection) config.GetSection("appSettings");
            if (appSettings.Settings.Count < 4)
            {
                throw new EzTextingClientException("Cannot read configuration file at: " + path + ".config");
            }
            return appSettings.Settings;
        }

        /// <summary>
        /// Performs GET request to specified path
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <param name="queryParams">query parameters</param>
        /// <returns>mapped object</returns>
        /// <exception cref="EzTextingApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="EzTextingClientException">      in case error has occurred in client.</exception>
        public EzTextingResponse<T> Get<T>(string path,
            IEnumerable<KeyValuePair<string, object>> queryParams) where T : new()
        {
            return Get<T>(path, null, queryParams);
        }

        /// <summary>
        /// Performs GET request to specified path
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <param name="request">API request object</param>
        /// <param name="queryParams">query parameters</param>
        /// <returns>mapped object</returns>
        /// <exception cref="EzTextingApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="EzTextingClientException">      in case error has occurred in client.</exception>
        public EzTextingResponse<T> Get<T>(string path, EzTextingModel request = null,
            IEnumerable<KeyValuePair<string, object>> queryParams = null) where T : new()
        {
            Logger.Debug("GET request to {0} with params: {1}", path, queryParams);
            var restRequest = CreateRestRequest(path, Method.GET, request, queryParams);
            return DoRequest<EzTextingResponse<T>>(restRequest);
        }

        /// <summary>
        /// Performs POST request with body to specified path
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <param name="queryParams">query parameters</param>
        /// <returns>mapped object</returns>
        /// <exception cref="EzTextingApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="EzTextingClientException">      in case error has occurred in client.</exception>
        public EzTextingResponse<T> Post<T>(string path,
            IEnumerable<KeyValuePair<string, object>> queryParams) where T : new()
        {
            return Post<T>(path, null, queryParams);
        }

        /// <summary>
        /// Performs POST request with body to specified path
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <param name="payload">object to send</param>
        /// <param name="queryParams">query parameters</param>
        /// <returns>mapped object</returns>
        /// <exception cref="EzTextingApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="EzTextingClientException">      in case error has occurred in client.</exception>
        public EzTextingResponse<T> Post<T>(string path, EzTextingModel payload = null,
            IEnumerable<KeyValuePair<string, object>> queryParams = null) where T : new()
        {
            Logger.Debug("POST request to {0} params: {1} entity: \n{2}", path, queryParams, payload);
            var restRequest = CreateRestRequest(path, Method.POST, payload, queryParams);
            return DoRequest<EzTextingResponse<T>>(restRequest);
        }

        /// <summary>
        /// Performs DELETE request to specified path with query parameters
        /// </summary>
        /// <param name="path">relative API request path</param>
        /// <param name="queryParams">query parameters</param>
        /// <exception cref="EzTextingApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="EzTextingClientException">      in case error has occurred in client.</exception>
        public void Delete(string path, IEnumerable<KeyValuePair<string, object>> queryParams = null)
        {
            Logger.Debug("DELETE request to {0} with params: {1}", path, queryParams);
            var restRequest = CreateRestRequest(path, Method.DELETE, null, queryParams);
            DoRequest<object>(restRequest);
        }

        private T DoRequest<T>(IRestRequest request) where T : new()
        {
            FilterRequest(request);
            var response = RestClient.Execute<T>(request);
            if (response.Content == null)
            {
                Logger.Debug("received http code {0} with null entity, returning null", response.StatusCode);
                return default(T);
            }
            VerifyResponse(response);
            Logger.Debug("received entity: {0}", response.Content);

            return response.Data;
        }

        private Stream DoRequest(IRestRequest request)
        {
            FilterRequest(request);
            Stream downloadedStream = new MemoryStream();
            request.ResponseWriter = (ms) => ms.CopyTo(downloadedStream);
            var response = RestClient.Execute(request);
            if (response.Content == null)
            {
                Logger.Debug("received http code {0} with null file data, returning null", response.StatusCode);
                return null;
            }
            Logger.Debug("received file data: {0}", response.Content);
            VerifyResponse(response);

            return downloadedStream;
        }

        private void VerifyResponse(IRestResponse response)
        {
            var statusCode = (int) response.StatusCode;
            if (statusCode < 400 && response.ErrorException == null) return;

            Logger.Error("request has failed: {0}", response.ErrorException);
            var errors = _jsonDeserializer.Deserialize<EzTextingResponse<object>>(response).Errors ??
                         new List<string> {response.ErrorException.Message};
            throw new EzTextingApiException(statusCode, errors);
/*
                TODO vmikhailov currently EZ API returns incorrect codes for almost all operations, so we just throw generic
                switch (statusCode)
                {
                    case 400:
                        throw new BadRequestException(message);
                    case 401:
                        throw new UnauthorizedException(message);
                    case 403:
                        throw new AccessForbiddenException(message);
                    case 404:
                        throw new ResourceNotFoundException(message);
                    case 500:
                        throw new InternalServerErrorException(message);
                    default:
                        throw new CallfireApiException(message);
                }
*/
        }

        private IRestRequest CreateRestRequest(string path, Method method, EzTextingModel model = null,
            IEnumerable<KeyValuePair<string, object>> queryParams = null)
        {
            var request = new RestRequest
            {
                Method = method,
                RequestFormat = DataFormat.Json,
                JsonSerializer = _jsonSerializer
            };
            var authString = _authentication.AsParamString();
            var requestPath = new StringBuilder(path);
            var modelQueryParams = ClientUtils.BuildQueryParams(model);
            var extraQueryParams = new StringBuilder();
            if (queryParams != null)
            {
                foreach (var param in queryParams)
                {
                    extraQueryParams
                        .Append(param.Key)
                        .Append("=")
                        .Append(param.Value.ToString().UrlEncode())
                        .Append('&');
                }
            }

            if (method == Method.GET || method == Method.DELETE)
            {
                requestPath.Append('&').Append(authString);
                CombineParameters(requestPath, modelQueryParams.ToString(), extraQueryParams.ToString());
            }
            else if (method == Method.POST)
            {
                var postBody = new StringBuilder(authString);
                CombineParameters(postBody, modelQueryParams.ToString(), extraQueryParams.ToString());
                request.AddParameter(ClientConstants.FormEncodedContentType, postBody.ToString(),
                    ClientConstants.FormEncodedContentType, ParameterType.RequestBody);
            }
            else
            {
                throw new EzTextingClientException("HTTP method " + method + " isn't supported.");
            }
            request.AddHeader("Accept", ClientConstants.JsonContentType);
            request.Resource = requestPath.ToString();

            return request;
        }

        private void CombineParameters(StringBuilder result, string paramString1, string paramString2)
        {
            if (paramString1.Length > 0)
            {
                result.Append('&');
                result.Append(paramString1);
                // remove trailing &
                result.Remove(result.Length - 1, 1);
            }
            if (paramString2.Length > 0)
            {
                result.Append('&');
                result.Append(paramString2);
                result.Remove(result.Length - 1, 1);
            }
        }

//            // makes extra deserialization to get pretty json string, enable only in case of debugging
//            private void logDebugPrettyJson(String message, Object... params) throws JsonProcessingException {
//                if (LOGGER.isDebugEnabled()) {
//                    for (int i = 0; i < params.length; i++) {
//                        if (params[i] instanceof EzTextingModel) {
//                            String prettyJson = jsonConverter.getMapper().writerWithDefaultPrettyPrinter()
//                                .writeValueAsString(params[i]);
//                            params[i] = prettyJson;
//                        }
//                    }
//                    LOGGER.debug(message, params);
//                }
//            }

        private void FilterRequest(IRestRequest request)
        {
            foreach (var filter in Filters)
            {
                filter.Filter(request);
            }
        }
    }
}