using System;
using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Api.Contacts.Model;
using EzTextingApiClient.Api.Groups.Model;
using EzTextingApiClient.Api.Messaging.Model;
using NUnit.Framework;
using RestSharp.Extensions;
using System.Collections.Generic;

namespace EzTextingApiClient.Tests
{
    [TestFixture]
    public class ClientUtilsTest
    {
        [Test]
        public void BuildQueryParams()
        {
            var now = DateTime.Now;
            var mms = new MmsMessage {
                FileId = 123,
                Subject = "test subject",
                Message = "this is mms message",
                Groups = new List<string> { "group1", "group2", "group3" },
                PhoneNumbers = new List<string> { "1234567890", "2345678900", "3456789000" },
                StampToSend = now
            };

            var queryParams = ClientUtils.BuildQueryParams(mms).ToString();

            Console.WriteLine("params: " + queryParams);

            Assert.That(queryParams, Is.StringContaining("FileID=123"));
            Assert.That(queryParams, Is.StringContaining("Groups[]=group1"));
            Assert.That(queryParams, Is.StringContaining("Groups[]=group2"));
            Assert.That(queryParams, Is.StringContaining("Groups[]=group3"));
            Assert.That(queryParams, Is.StringContaining("PhoneNumbers[]=1234567890"));
            Assert.That(queryParams, Is.StringContaining("PhoneNumbers[]=2345678900"));
            Assert.That(queryParams, Is.StringContaining("PhoneNumbers[]=3456789000"));
            Assert.That(queryParams, Is.StringContaining("Subject=" + "test subject".UrlEncode()));
            Assert.That(queryParams, Is.StringContaining("Message=" + "this is mms message".UrlEncode()));
            Assert.That(queryParams, Is.StringContaining("MessageTypeID=3"));
            Assert.That(queryParams, Is.StringContaining("StampToSend=" + ClientUtils.ToUnixTime(now) / 1000L));
        }

        [Test]
        public void BuildQueryParamsFromGetRequest()
        {
            var request = new GetGroupsRequest {
                SortBy = SortProperty.Name,
                SortType = SortType.Asc,
                ItemsPerPage = 10,
                Page = 5
            };
            var queryParams = ClientUtils.BuildQueryParams(request).ToString();

            Console.WriteLine("get request: " + queryParams);

            Assert.That(queryParams, Is.StringContaining("sortBy=Name"));
            Assert.That(queryParams, Is.StringContaining("sortDir=asc"));
            Assert.That(queryParams, Is.StringContaining("itemsPerPage=10"));
            Assert.That(queryParams, Is.StringContaining("page=5"));
        }

        [Test]
        public void BuildQueryParamsWithBooleanAsNumber()
        {
            var contact = new Contact {
                Email = "email@email.com",
                OptOut = true
            };

            var queryParams = ClientUtils.BuildQueryParams(contact).ToString();
            Console.WriteLine("contact: " + queryParams);
            Assert.That(queryParams, Is.StringContaining("Email=" + "email@email.com".UrlEncode()));
            Assert.That(queryParams, Is.StringContaining("OptOut=1"));
        }
    }
}