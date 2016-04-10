using System;
using System.Collections.Generic;
using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Api.Credits.Model;
using EzTextingApiClient.Api.Keywords.Model;
using EzTextingApiClient.Api.Messaging.Model;
using NUnit.Framework;
using RestSharp;
using RestSharp.Extensions;

namespace EzTextingApiClient.Tests.Api
{
    [TestFixture]
    public class KeywordsApiTest : AbstractApiTest
    {
        [Test]
        public void CheckAvailabilityBlankKeyword()
        {
            var restRequest = MockRestResponse();
            var ex = Assert.Throws<ArgumentException>(() => Client.KeywordsApi.CheckAvailability(" "));
            Assert.That(ex.Message, Is.EqualTo("keyword cannot be blank"));
        }

        [Test]
        public void CheckAvailability()
        {
            var expectedJson = GetJsonPayload("/keywords/keywordsApi/checkAvailability.json");
            var restRequest = MockRestResponse(expectedJson);
            var response = new CheckAvailabilityResponse("honey", true);
            var avail = Client.KeywordsApi.CheckAvailability("NewKw");
            Assert.IsTrue(avail);
            EzTextingResponse<CheckAvailabilityResponse> ezResponse = new EzTextingResponse<CheckAvailabilityResponse>(
                "Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/new?Keyword=NewKw&"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
        }

        [Test]
        public void SetupBlankKeyword()
        {
            var restRequest = MockRestResponse();
            var ex = Assert.Throws<ArgumentException>(() => Client.KeywordsApi.Setup(new Keyword()));
            Assert.That(ex.Message, Is.EqualTo("keyword cannot be blank"));
        }

        [Test]
        public void Setup()
        {
            var expectedJson = GetJsonPayload("/keywords/keywordsApi/setup.json");
            var restRequest = MockRestResponse(expectedJson);

            var joinMsg = new SimpleMessage
            {
                DeliveryMethod = DeliveryMethod.Express,
                Subject = "subject",
                Message = "Thank you for joining our text list."
            };
            var confirmMsg = new SimpleMessage
            {
                DeliveryMethod = DeliveryMethod.Express,
                Subject = "subject",
                Message = "You already joined but thanks for texting in again."
            };
            var alternateReply = new SimpleMessage
            {
                DeliveryMethod = DeliveryMethod.Express,
                Subject = "subject",
                Message = "You already joined but thanks for texting in again."
            };
            var keyword = new Keyword
            {
                Value = "NewKw",
                Id = 147258369,
                EnableDoubleOptIn = true,
                EnableAlternateReply = true,
                ConfirmMessage = confirmMsg,
                JoinMessage = joinMsg,
                AlternateReply = alternateReply,
                ForwardEmail = "honey@bear-alliance.co.uk",
                ForwardUrl = "http://bear-alliance.co.uk/honey-donations/",
                ContactGroups = new List<string> {"Friends", "Family"}
            };

            var response = Client.KeywordsApi.Setup(keyword);

            EzTextingResponse<Keyword> ezResponse = new EzTextingResponse<Keyword>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/keywords/NewKw?"));

            Assert.That(bodyParam.Value, Is.StringContaining("Keyword=NewKw"));
            Assert.That(bodyParam.Value, Is.StringContaining("EnableDoubleOptIn=true"));
            Assert.That(bodyParam.Value, Is.StringContaining("EnableAlternateReply=true"));
            Assert.That(bodyParam.Value, Is.StringContaining("ConfirmMessage="));
            Assert.That(bodyParam.Value, Is.StringContaining("JoinMessage="));
            Assert.That(bodyParam.Value, Is.StringContaining("ForwardEmail=" + "honey@bear-alliance.co.uk".UrlEncode()));
            Assert.That(bodyParam.Value,
                Is.StringContaining("ForwardUrl=" + "http://bear-alliance.co.uk/honey-donations/".UrlEncode()));
            Assert.That(bodyParam.Value, Is.StringContaining("ContactGroupIDs[]=Friends"));
            Assert.That(bodyParam.Value, Is.StringContaining("ContactGroupIDs[]=Family"));

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));
        }

        [Test]
        public void RentWithNewCreditCardBlankKeyword()
        {
            var restRequest = MockRestResponse();
            var ex = Assert.Throws<ArgumentException>(() => Client.KeywordsApi.Rent(" ", new CreditCard()));
            Assert.That(ex.Message, Is.EqualTo("keyword cannot be blank"));
        }

        [Test]
        public void RentWithNewCreditCard()
        {
            var expectedJson = GetJsonPayload("/keywords/keywordsApi/rent.json");
            var restRequest = MockRestResponse(expectedJson);
            var cc = new CreditCard
            {
                FirstName = "John",
                LastName = "Doe",
                State = "LA",
                SecurityCode = "123",
                ExpirationMonth = "11",
                ExpirationYear = "2020",
                Number = "4111111111111111",
                City = "LA",
                Country = "US",
                Street = "1 Avenue",
                Zip = "31331",
                Type = CreditCardType.MasterCard
            };

            var response = Client.KeywordsApi.Rent("NewKw", cc);

            EzTextingResponse<Keyword> ezResponse = new EzTextingResponse<Keyword>("Success", 201, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/keywords?format=json"));

            Assert.That(bodyParam.Value, Is.StringContaining("Keyword=NewKw"));

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

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));
        }

        [Test]
        public void RentWithStoredCreditCardBlankKeyword()
        {
            var restRequest = MockRestResponse();
            var ex = Assert.Throws<ArgumentException>(() => Client.KeywordsApi.Rent(null, "1234"));
            Assert.That(ex.Message, Is.EqualTo("keyword cannot be blank"));
            ex = Assert.Throws<ArgumentException>(() => Client.KeywordsApi.Rent("NewKw", " "));
            Assert.That(ex.Message, Is.EqualTo("ccNumber cannot be blank"));
        }

        [Test]
        public void RentWithStoredCreditCard()
        {
            var expectedJson = GetJsonPayload("/keywords/keywordsApi/rent.json");
            var restRequest = MockRestResponse(expectedJson);

            var response = Client.KeywordsApi.Rent("NewKw", "1234");

            EzTextingResponse<Keyword> ezResponse = new EzTextingResponse<Keyword>("Success", 201, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/keywords?format=json"));

            Assert.That(bodyParam.Value, Is.StringContaining("Keyword=NewKw"));
            Assert.That(bodyParam.Value, Is.StringContaining("StoredCreditCard=1234"));

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));
        }

        [Test]
        public void CancelBlankKeyword()
        {
            var restRequest = MockRestResponse();
            var ex = Assert.Throws<ArgumentException>(() => Client.KeywordsApi.Cancel(" "));
            Assert.That(ex.Message, Is.EqualTo("keyword cannot be blank"));
        }

        [Test]
        public void Cancel()
        {
            var restRequest = MockRestResponse();
            Client.KeywordsApi.Cancel("TestKw");

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/keywords/TestKw?"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
        }
    }
}