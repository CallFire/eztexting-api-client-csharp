using System;
using System.Collections.Generic;
using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Api.Groups.Model;
using EzTextingApiClient.Api.Inbox.Model;
using NUnit.Framework;
using RestSharp;
using RestSharp.Extensions;
using SortProperty = EzTextingApiClient.Api.Inbox.Model.SortProperty;

namespace EzTextingApiClient.Tests.Api
{
    [TestFixture]
    public class InboxApiTest : AbstractApiTest
    {
        [Test]
        public void GetMessages()
        {
            var expectedJson = GetJsonPayload("/inbox/inboxApi/getMessages.json");
            var restRequest = MockRestResponse(expectedJson);
            var request = new GetMessagesRequest
            {
                Search = "msg",
                FolderId = 20,
                Type = MessageType.Sms,
                SortType = SortType.Desc,
                SortBy = SortProperty.ReceivedOn,
                ItemsPerPage = 10,
                Page = 1
            };

            var response = Client.InboxApi.GetMessages(request);
            EzTextingResponse<InboxMessage> ezResponse = new EzTextingResponse<InboxMessage>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));

            Assert.That(restRequest.Value.Resource, Is.StringContaining("Search=msg"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("FolderID=20"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Filter=SMS"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("sortBy=ReceivedOn"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("sortDir=desc"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("itemsPerPage=10"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("page=1"));
        }

        [Test]
        public void GetMessage()
        {
            var expectedJson = GetJsonPayload("/inbox/inboxApi/getMessage.json");
            var restRequest = MockRestResponse(expectedJson);
            var response = Client.InboxApi.GetMessage(10);
            EzTextingResponse<InboxMessage> ezResponse = new EzTextingResponse<InboxMessage>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/10?"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
        }

        [Test]
        public void DeleteMessage()
        {
            var restRequest = MockRestResponse();
            Client.InboxApi.DeleteMessage(10);

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/incoming-messages/10?format=json"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
        }

        [Test]
        public void MoveMessage()
        {
            var restRequest = MockRestResponse();

            Client.InboxApi.MoveMessage(10, 20);

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));

            Assert.That(bodyParam.Value, Is.StringContaining("ID=10"));
            Assert.That(bodyParam.Value, Is.StringContaining("FolderID=20"));
        }

        [Test]
        public void MoveMessages()
        {
            var restRequest = MockRestResponse();

            Client.InboxApi.MoveMessages(new List<long> {1, 2, 3}, 20);

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));

            Assert.That(bodyParam.Value, Is.StringContaining("ID[]=1"));
            Assert.That(bodyParam.Value, Is.StringContaining("ID[]=2"));
            Assert.That(bodyParam.Value, Is.StringContaining("ID[]=3"));
            Assert.That(bodyParam.Value, Is.StringContaining("FolderID=20"));
        }

        [Test]
        public void CreateFolder()
        {
            var expectedJson = GetJsonPayload("/inbox/inboxApi/createFolder.json");
            var restRequest = MockRestResponse(expectedJson);

            var response = Client.InboxApi.CreateFolder("test folder");
            // set name to null because serer returns only id
            response.Name = null;
            EzTextingResponse<Folder> ezResponse = new EzTextingResponse<Folder>("Success", 201, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));
            Assert.That(bodyParam.Value, Is.StringContaining("Name=" + "test folder".UrlEncode()));
        }

        [Test]
        public void CreateFolderBlankName()
        {
            var ex = Assert.Throws<ArgumentException>(() => Client.InboxApi.CreateFolder(" "));
            Assert.That(ex.Message, Is.EqualTo("name cannot be blank"));
        }

        [Test]
        public void UpdateFolder()
        {
            var restRequest = MockRestResponse();
            var folder = new Folder
            {
                Id = 10,
                Name = "folder 1"
            };

            Client.InboxApi.UpdateFolder(folder);

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/10?"));

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));
            Assert.That(bodyParam.Value, Is.StringContaining("Name=" + "folder 1".UrlEncode()));
        }

        [Test]
        public void UpdateFolderNullId()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Client.InboxApi.UpdateFolder(new Folder()));
            Assert.That(ex.Message, Is.StringContaining("Value cannot be null"));
        }

        [Test]
        public void GetFolder()
        {
            var expectedJson = GetJsonPayload("/inbox/inboxApi/getFolder.json");
            var restRequest = MockRestResponse(expectedJson);
            var response = Client.InboxApi.GetFolder(10);
            EzTextingResponse<Folder> ezResponse = new EzTextingResponse<Folder>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/10?"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
        }

        [Test]
        public void GetFolders()
        {
            var expectedJson = GetJsonPayload("/inbox/inboxApi/getFolders.json");
            var restRequest = MockRestResponse(expectedJson);
            var response = Client.InboxApi.GetFolders();
            EzTextingResponse<Folder> ezResponse = new EzTextingResponse<Folder>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/messages-folders?format=json"));
        }

        [Test]
        public void DeleteFolder()
        {
            var restRequest = MockRestResponse();
            Client.InboxApi.DeleteFolder(10);

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/10?"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
        }
    }
}