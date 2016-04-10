using System.Collections.Generic;
using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Api.Groups.Model;
using NUnit.Framework;
using RestSharp;
using RestSharp.Extensions;

namespace EzTextingApiClient.Tests.Api
{
    [TestFixture]
    public class GroupsApiTest : AbstractApiTest
    {
        [Test]
        public void Create()
        {
            var expectedJson = GetJsonPayload("/groups/groupsApi/create.json");
            var restRequest = MockRestResponse(expectedJson);
            var group = new Group
            {
                Name = "group 2",
                Note = "note 2"
            };

            var response = Client.GroupsApi.Create(group);
            EzTextingResponse<Group> ezResponse = new EzTextingResponse<Group>("Success", 201, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));
            Assert.That(bodyParam.Value, Is.StringContaining("Name=" + "group 2".UrlEncode()));
            Assert.That(bodyParam.Value, Is.StringContaining("Note=" + "note 2".UrlEncode()));
        }

        [Test]
        public void Update()
        {
            var expectedJson = GetJsonPayload("/groups/groupsApi/update.json");
            var restRequest = MockRestResponse(expectedJson);
            var group = new Group
            {
                Id = 10,
                Name = "group 2",
                Note = "note 2"
            };

            var response = Client.GroupsApi.Update(group);
            EzTextingResponse<Group> ezResponse = new EzTextingResponse<Group>("Success", 201, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/10?"));

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));
            Assert.That(bodyParam.Value, Is.StringContaining("Name=" + "group 2".UrlEncode()));
            Assert.That(bodyParam.Value, Is.StringContaining("Note=" + "note 2".UrlEncode()));
        }

        [Test]
        public void Get()
        {
            var expectedJson = GetJsonPayload("/groups/groupsApi/get.json");
            var restRequest = MockRestResponse(expectedJson);
            var response = Client.GroupsApi.Get(10);
            EzTextingResponse<Group> ezResponse = new EzTextingResponse<Group>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/10?"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
        }

        [Test]
        public void GetAll()
        {
            var expectedJson = GetJsonPayload("/groups/groupsApi/getAllGroups.json");
            var restRequest = MockRestResponse(expectedJson);
            var getRequest = new GetGroupsRequest
            {
                SortBy = SortProperty.Name,
                SortType = SortType.Desc,
                ItemsPerPage = 10,
                Page = 7
            };
            var response = Client.GroupsApi.Get(getRequest);
            EzTextingResponse<Group> ezResponse = new EzTextingResponse<Group>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));

            Assert.That(restRequest.Value.Resource, Is.StringContaining("sortBy=Name"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("sortDir=desc"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("itemsPerPage=10"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("page=7"));
        }

        [Test]
        public void Delete()
        {
            var restRequest = MockRestResponse();
            Client.GroupsApi.Delete(10);

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/10?"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
        }
    }
}