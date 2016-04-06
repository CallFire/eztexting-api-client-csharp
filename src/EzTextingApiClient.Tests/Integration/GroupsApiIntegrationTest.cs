using System;
using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Api.Contacts.Model;
using EzTextingApiClient.Api.Groups.Model;
using NUnit.Framework;
using SortProperty = EzTextingApiClient.Api.Groups.Model.SortProperty;

namespace EzTextingApiClient.Tests.Integration
{
    [TestFixture, Ignore("temporary disabled")]
    public class GroupsApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void CrudOperations()
        {
            var group = new Group
            {
                Name = "group 2",
                Note = "note 2"
            };

            var created = Client.GroupsApi.Create(group);
            Assert.NotNull(created.Id);

            Console.WriteLine("created group: " + created);

            created.Name += "_upd";
            var updated = Client.GroupsApi.Update(created);
            Assert.NotNull(updated.Id);
            Assert.AreEqual(created.Name, updated.Name);
            Console.WriteLine("updated group: " + created);

            updated = Client.GroupsApi.Get((long) updated.Id);
            Console.WriteLine("get updated group: " + updated);

            var request = new GetGroupsRequest
            {
                SortType = SortType.Desc,
                ItemsPerPage = 2,
                Page = 0
            };
            var groups = Client.GroupsApi.Get(request);
            Console.WriteLine("get all groups: " + string.Join(",", groups));

            Client.GroupsApi.Delete((long) created.Id);
            var ex = Assert.Throws<EzTextingApiException>(() => Client.GroupsApi.Get((long) created.Id));
            Assert.That(ex.Message, Is.EqualTo("Sorry, nothing was found"));
        }
    }
}