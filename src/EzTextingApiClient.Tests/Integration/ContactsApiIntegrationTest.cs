using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EzTextingApiClient;
using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Api.Contacts.Model;
using EzTextingApiClient.Tests.Integration;
using NUnit.Framework;

namespace EzTextingApiClient.Tests.Integration
{
    [TestFixture, Ignore("temporary ignored")]
    public class ContactsApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void CrudOperations()
        {
            var contact = new Contact
            {
                FirstName = "Piglet",
                LastName = "Notail",
                PhoneNumber = "2123456788",
                Email = "piglet@small-animals-alliance.org",
                Note = "It is hard to be brave, when you are only a Very Small Animal.",
                Groups = new List<string>() {"Friends", "Neighbors"}
            };

            var created = Client.ContactsApi.Create(contact);

            Assert.Null(created.OptOut);
            Console.WriteLine("created contact: " + created);

            created.FirstName += "_upd";
            // opted out contacts cannot be updated or deleted
            // created.OptOut = true;
            var updated = Client.ContactsApi.Update(created);
            Assert.AreEqual(created.FirstName, updated.FirstName);
            Console.WriteLine("updated contact: " + created);

            updated = Client.ContactsApi.Get(updated.Id);
            Console.WriteLine("get updated contact: " + created);

            var request = new GetContactsRequest
            {
//                Query = QueryProperty.FirstName,
//                Source = SourceType.Manual,
//                OptOut = false,
                Group = "Friends",
                SortBy = SortProperty.CreatedAt,
                SortType = SortType.Asc,
                ItemsPerPage = 2,
                Page = 0
            };
            var contacts = Client.ContactsApi.Get(request);
            Console.WriteLine("get all contacts: " + string.Join(",", contacts));

            Client.ContactsApi.Delete(created.Id);
            var ex = Assert.Throws<EzTextingApiException>(() => Client.ContactsApi.Get(created.Id));
            Assert.That(ex.Message, Is.EqualTo("Sorry, nothing was found"));
        }
    }
}