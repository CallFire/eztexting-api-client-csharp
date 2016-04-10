using System;
using System.Collections.Generic;
using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Api.Inbox.Model;
using NUnit.Framework;

namespace EzTextingApiClient.Tests.Integration
{
    [TestFixture, Ignore("temporary disabled")]
    public class InboxApiIntegrationTest : AbstractIntegrationTest
    {
        public const long FriendsFolderId = 8308;
        public const long InboxMsgId = 20003043;

        [Test]
        public void MoveMessage()
        {
            Client.InboxApi.MoveMessage(InboxMsgId, FriendsFolderId);
        }

        [Test]
        public void MoveMessages()
        {
            Client.InboxApi.MoveMessages(new List<long> {InboxMsgId}, FriendsFolderId);
        }

        [Test]
        public void GetMessage()
        {
            var msg = Client.InboxApi.GetMessage(InboxMsgId);
            Console.WriteLine(msg);
        }

        [Test]
        public void DeleteMessage()
        {
            Client.InboxApi.DeleteMessage(InboxMsgId);
        }

        [Test]
        public void GetMessages()
        {
            var request = new GetMessagesRequest
            {
                Search = "test",
                FolderId = 8308,
                Type = MessageType.Sms,
                SortType = SortType.Desc,
                SortBy = SortProperty.ReceivedOn,
                ItemsPerPage = 10,
                Page = 0
            };
            var messages = Client.InboxApi.GetMessages(request);
            Console.WriteLine("inbox messages: " + string.Join(",", messages));
        }

        [Test]
        public void FoldersCrudOperations()
        {
            var folder = Client.InboxApi.CreateFolder("testfolder");
            Console.WriteLine("created folder: " + folder);
            Assert.NotNull(folder.Id);

            folder.Name += "_updated";
            Client.InboxApi.UpdateFolder(folder);

            var updated = Client.InboxApi.GetFolder(folder.Id.Value);
            Console.WriteLine("updated folder: " + folder);
            Assert.AreEqual(folder.Name, updated.Name);

            var folders = Client.InboxApi.GetFolders();
            Console.WriteLine("all folders: " + string.Join(",", folders));

            Client.InboxApi.DeleteFolder(folder.Id.Value);

            var ex = Assert.Throws<EzTextingApiException>(() => Client.InboxApi.GetFolder(folder.Id.Value));
            Assert.That(ex.Message, Is.EqualTo("Sorry, nothing was found"));
        }
    }
}