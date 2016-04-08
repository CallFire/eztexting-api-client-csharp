using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Api.Credits.Model;
using EzTextingApiClient.Api.Inbox.Model;
using NUnit.Framework;
using RestSharp;
using RestSharp.Extensions;

namespace EzTextingApiClient.Tests.Api
{
    [TestFixture]
    public class CreditsApiTest : AbstractApiTest
    {
        [Test]
        public void CheckBalance()
        {
            var expectedJson = GetJsonPayload("/credits/creditsApi/checkBalance.json");
            var restRequest = MockRestResponse(expectedJson);
            var response = Client.CreditsApi.CheckBalance();
            EzTextingResponse<CreditBalance> ezResponse = new EzTextingResponse<CreditBalance>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/billing/credits/get?format=json"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
        }

        [Test]
        public void BuyCreditsUsingStoredCreditCard()
        {
            var expectedJson = GetJsonPayload("/credits/creditsApi/buyCredits.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new BuyCreditsRequest
            {
                Credits = 2000,
                CouponCode = "ABX32WE",
                StoredCard = "4533"
            };
            var response = Client.CreditsApi.BuyCredits(request);
            EzTextingResponse<BuyCreditsResponse> ezResponse = new EzTextingResponse<BuyCreditsResponse>(
                "Success", 201, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));

            Assert.That(bodyParam.Value, Is.StringContaining("NumberOfCredits=2000"));
            Assert.That(bodyParam.Value, Is.StringContaining("CouponCode=ABX32WE"));
            Assert.That(bodyParam.Value, Is.StringContaining("StoredCreditCard=4533"));
        }

        [Test]
        public void BuyCreditsUsingNewCreditCard()
        {
            var expectedJson = GetJsonPayload("/credits/creditsApi/buyCredits.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new BuyCreditsRequest
            {
                Credits = 2000,
                CouponCode = "ABX32WE",
                FirstName = "John",
                LastName = "Doe",
                State = "LA",
                SecurityCode = "123",
                ExpirationMonth = "11",
                ExpirationYear = "2020",
                Number = "411111111111",
                City = "LA",
                Country = "US",
                Street = "1 Avenue",
                Zip = "31331",
                Type = CreditCardType.MasterCard
            };
            var response = Client.CreditsApi.BuyCredits(request);
            EzTextingResponse<BuyCreditsResponse> ezResponse = new EzTextingResponse<BuyCreditsResponse>(
                "Success", 201, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));

            Assert.That(bodyParam.Value, Is.StringContaining("NumberOfCredits=2000"));
            Assert.That(bodyParam.Value, Is.StringContaining("CouponCode=ABX32WE"));

            Assert.That(bodyParam.Value, Is.StringContaining("FirstName=John"));
            Assert.That(bodyParam.Value, Is.StringContaining("LastName=Doe"));
            Assert.That(bodyParam.Value, Is.StringContaining("State=LA"));
            Assert.That(bodyParam.Value, Is.StringContaining("City=LA"));
            Assert.That(bodyParam.Value, Is.StringContaining("Street=" + "1 Avenue".UrlEncode()));
            Assert.That(bodyParam.Value, Is.StringContaining("Country=US"));
            Assert.That(bodyParam.Value, Is.StringContaining("State=LA"));
            Assert.That(bodyParam.Value, Is.StringContaining("SecurityCode=123"));
            Assert.That(bodyParam.Value, Is.StringContaining("ExpirationMonth=11"));
            Assert.That(bodyParam.Value, Is.StringContaining("ExpirationYear=2020"));
            Assert.That(bodyParam.Value, Is.StringContaining("Number=411111111111"));
            Assert.That(bodyParam.Value, Is.StringContaining("Zip=31331"));
            Assert.That(bodyParam.Value, Is.StringContaining("CreditCardTypeID=MasterCard"));
        }
    }
}