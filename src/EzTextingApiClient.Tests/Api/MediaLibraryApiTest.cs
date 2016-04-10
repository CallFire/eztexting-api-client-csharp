using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Api.Media.Model;
using NUnit.Framework;
using RestSharp;
using RestSharp.Extensions;

namespace EzTextingApiClient.Tests.Api
{
    [TestFixture]
    public class MediaLibraryApiTest : AbstractApiTest
    {
        [Test]
        public void Create()
        {
            var expectedJson = GetJsonPayload("/media/mediaLibraryApi/get.json");
            var restRequest = MockRestResponse(expectedJson);
            var response = Client.MediaLibraryApi.Create("http://file-storage.com/file.wav");
            EzTextingResponse<MediaFile> ezResponse = new EzTextingResponse<MediaFile>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(bodyParam.Value, Is.StringContaining("Source=" + "http://file-storage.com/file.wav".UrlEncode()));

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));
        }

        [Test]
        public void Get()
        {
            var expectedJson = GetJsonPayload("/media/mediaLibraryApi/get.json");
            var restRequest = MockRestResponse(expectedJson);
            var response = Client.MediaLibraryApi.Get(10);
            EzTextingResponse<MediaFile> ezResponse = new EzTextingResponse<MediaFile>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/10?"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
        }

        [Test]
        public void GetAll()
        {
            var expectedJson = GetJsonPayload("/media/mediaLibraryApi/getAllFiles.json");
            var restRequest = MockRestResponse(expectedJson);
            var getRequest = new GetRequest
            {
                SortType = SortType.Desc,
                ItemsPerPage = 10,
                Page = 7
            };
            var response = Client.MediaLibraryApi.Get(getRequest);
            EzTextingResponse<MediaFile> ezResponse = new EzTextingResponse<MediaFile>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));

            Assert.That(restRequest.Value.Resource, Is.StringContaining("sortDir=desc"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("itemsPerPage=10"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("page=7"));
        }

        [Test]
        public void Delete()
        {
            var restRequest = MockRestResponse();
            Client.MediaLibraryApi.Delete(10);

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/10?"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
        }
    }
}