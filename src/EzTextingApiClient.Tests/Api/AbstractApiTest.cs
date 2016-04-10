using System;
using System.IO;
using System.Net;
using System.Text;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace EzTextingApiClient.Tests.Api
{
    public class AbstractApiTest
    {
        protected const string BasePath = "../../JsonMocks";
        protected EzTextingClient Client;
        protected ISerializer Serializer;
        protected IDeserializer Deserializer;

        public AbstractApiTest()
        {
            Client = new EzTextingClient("login", "password");
            Serializer = new EzTextingJsonConverter();
            Deserializer = Serializer as IDeserializer;
        }

        protected string GetJsonPayload(string path)
        {
            var result = new StringBuilder();
            var jsonLines = File.ReadAllLines(BasePath + path);
            foreach (var line in jsonLines) {
                var formatted = line.Trim();
                result.Append(formatted.Replace(": ", ":"));
            }
            return result.ToString();
        }

        protected Ref<IRestRequest> MockRestResponse(string responseData = "",
                                                     HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var payload = Encoding.ASCII.GetBytes(responseData);
            Client.RestApiClient.RestClient = new MockRestClient(Client.RestApiClient.RestClient, Deserializer,
                new HttpResponse {
                    StatusCode = statusCode,
                    RawBytes = payload,
                    ContentLength = payload.Length
                });
            return ((MockRestClient)Client.RestApiClient.RestClient).CapturedRequest;
        }
    }
}