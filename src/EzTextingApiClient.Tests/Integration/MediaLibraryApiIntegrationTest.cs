using System;
using EzTextingApiClient.Api.Common.Model;
using NUnit.Framework;

namespace EzTextingApiClient.Tests.Integration
{
    //before running this tests setup username and password in AbstractIntegrationTest.cs
    [TestFixture, Ignore("temporary disabled")]
    public class MediaLibraryApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void CrudOperations()
        {
            const string path = "https://www.eztexting.com/sites/all/themes/ez/images/ez-texting-logo.png";
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
        
        [Test]
        public void Upload()
        {
            const string wavFilePath = "Resources/train1.wav";
            var created = Client.MediaLibraryApi.Upload(wavFilePath);
            Assert.NotNull(created.Id);
            
            Console.WriteLine("created media: " + created);
            
            var get = Client.MediaLibraryApi.Get(created.Id.Value);
            Console.WriteLine("get media: " + get);
            
            Client.MediaLibraryApi.Delete((long) created.Id);
            var ex = Assert.Throws<EzTextingApiException>(() => Client.MediaLibraryApi.Get((long) created.Id));
            Assert.That(ex.Message, Is.EqualTo("Sorry, nothing was found"));
        }
    }
}