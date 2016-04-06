using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Api.Messaging.Model;
using EzTextingApiClient.Api.Toolbox.Model;
using NUnit.Framework;
using RestSharp;

namespace EzTextingApiClient.Tests.Api.Toolbox
{
    [TestFixture]
    public class ToolboxApiTest : AbstractApiTest
    {
        [Test]
        public void CarrierLookup()
        {
            var expectedJson = GetJsonPayload("/toolbox/toolboxApi/carrierLookup.json");
            var restRequest = MockRestResponse(expectedJson);

            var response = Client.ToolboxApi.CarrierLookup("12345678901");
            var ezResponse = new EzTextingResponse<CarrierLookupResponse>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/sending/phone-numbers/12345678901?format="));
        }
    }
}