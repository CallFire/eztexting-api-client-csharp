using System;
using EzTextingApiClient.Api.Common.Model;
using NUnit.Framework;

namespace EzTextingApiClient.Tests.Integration
{
    [TestFixture]
    public class MediaLibraryApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void CrudOperations()
        {
            const string path = "https://eztxting.s3.amazonaws.com/188814/mms/train_1449507791.mp3";
            var created = Client.MediaLibraryApi.Create(path);
            Assert.NotNull(created.Id);

            Console.WriteLine("created media: " + created);

            var get = Client.MediaLibraryApi.Get(created.Id.Value);
            Console.WriteLine("get media: " + get);

            var request = new GetRequest
            {
                SortType = SortType.Desc,
                ItemsPerPage = 2,
                Page = 0
            };
            var allMedia = Client.MediaLibraryApi.Get(request);
            Console.WriteLine("get all media: " + string.Join(",", allMedia));

            Client.MediaLibraryApi.Delete((long) created.Id);
            var ex = Assert.Throws<EzTextingApiException>(() => Client.MediaLibraryApi.Get((long) created.Id));
            Assert.That(ex.Message, Is.EqualTo("Sorry, nothing was found"));
        }
    }
}