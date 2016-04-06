using System.Collections.Generic;
using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Api.Contacts.Model;
using EzTextingApiClient.Api.Messaging.Model;
using NUnit.Framework;
using RestSharp;
using RestSharp.Extensions;

namespace EzTextingApiClient.Tests.Api
{
    [TestFixture]
    public class ContactsApiTest : AbstractApiTest
    {
        [Test]
        public void Create()
        {
            var expectedJson = GetJsonPayload("/contacts/contactsApi/create.json");
            var restRequest = MockRestResponse(expectedJson);
            var contact = new Contact
            {
                FirstName = "Piglet",
                LastName = "Notail",
                PhoneNumber = "2123456785",
                Email = "piglet@small-animals-alliance.org",
                Note = "It is hard to be brave, when you are only a Very Small Animal.",
                Groups = new List<string>() {"Friends", "Neighbors"}
            };

            var response = Client.ContactsApi.Create(contact);
            EzTextingResponse<Contact> ezResponse = new EzTextingResponse<Contact>("Success", 201, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));
            Assert.That(bodyParam.Value, Is.StringContaining("FirstName=Piglet"));
            Assert.That(bodyParam.Value, Is.StringContaining("LastName=Notail"));
            Assert.That(bodyParam.Value, Is.StringContaining("PhoneNumber=2123456785"));
            Assert.That(bodyParam.Value, Is.StringContaining("Email=" + "piglet@small-animals-alliance.org".UrlEncode()));
            Assert.That(bodyParam.Value, Is.StringContaining(
                "Note=" + "It is hard to be brave, when you are only a Very Small Animal.".UrlEncode()));
            Assert.That(bodyParam.Value, Is.StringContaining("Groups[]=Friends"));
            Assert.That(bodyParam.Value, Is.StringContaining("Groups[]=Neighbors"));
        }

        [Test]
        public void Update()
        {
            var expectedJson = GetJsonPayload("/contacts/contactsApi/update.json");
            var restRequest = MockRestResponse(expectedJson);
            var contact = new Contact
            {
                Id = "4f0b5720734fada368000000",
                OptOut = true,
                FirstName = "Piglet",
                LastName = "Notail",
                PhoneNumber = "2123456785",
                Email = "piglet@small-animals-alliance.org",
                Note = "It is hard to be brave, when you are only a Very Small Animal.",
                Groups = new List<string>() {"Friends", "Neighbors"}
            };

            var response = Client.ContactsApi.Update(contact);
            EzTextingResponse<Contact> ezResponse = new EzTextingResponse<Contact>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/4f0b5720734fada368000000?"));

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));
            Assert.That(bodyParam.Value, Is.StringContaining("FirstName=Piglet"));
            Assert.That(bodyParam.Value, Is.StringContaining("LastName=Notail"));
            Assert.That(bodyParam.Value, Is.StringContaining("PhoneNumber=2123456785"));
            Assert.That(bodyParam.Value, Is.StringContaining("Email=" + "piglet@small-animals-alliance.org".UrlEncode()));
            Assert.That(bodyParam.Value, Is.StringContaining(
                "Note=" + "It is hard to be brave, when you are only a Very Small Animal.".UrlEncode()));
            Assert.That(bodyParam.Value, Is.StringContaining("Groups[]=Friends"));
            Assert.That(bodyParam.Value, Is.StringContaining("Groups[]=Neighbors"));
        }

        [Test]
        public void Get()
        {
            var expectedJson = GetJsonPayload("/contacts/contactsApi/get.json");
            var restRequest = MockRestResponse(expectedJson);
            var response = Client.ContactsApi.Get("4f0b5720734fada368000000");
            EzTextingResponse<Contact> ezResponse = new EzTextingResponse<Contact>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/4f0b5720734fada368000000?"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
        }

        [Test]
        public void GetAll()
        {
            var expectedJson = GetJsonPayload("/contacts/contactsApi/getAllContacts.json");
            var restRequest = MockRestResponse(expectedJson);
            var getRequest = new GetContactsRequest
            {
                Query = QueryProperty.FirstName,
                Source = SourceType.Manual,
                OptOut = true,
                Group = "My Friends",
                SortBy = SortProperty.CreatedAt,
                SortType = SortType.Desc,
                ItemsPerPage = 10,
                Page = 7
            };
            var response = Client.ContactsApi.Get(getRequest);
            EzTextingResponse<Contact> ezResponse = new EzTextingResponse<Contact>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));

            Assert.That(restRequest.Value.Resource, Is.StringContaining("query=FirstName"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("source=" + "Manually Added".UrlEncode()));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("group=" + "My Friends".UrlEncode()));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("optout=true"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("sortBy=CreatedAt"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("sortDir=desc"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("itemsPerPage=10"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("page=7"));
        }

        [Test]
        public void Delete()
        {
            var restRequest = MockRestResponse();
            Client.ContactsApi.Delete("4f0b5720734fada368000000");

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/4f0b5720734fada368000000?"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
        }
    }
}