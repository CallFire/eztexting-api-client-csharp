using System;
using System.Collections.Generic;
using System.Linq;
using EzTextingApiClient.Api.Messaging.Model;
using NUnit.Framework;

namespace EzTextingApiClient.Tests.Integration
{
    [TestFixture, Ignore("temporary disabled")]
    public class MessagingIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void SendSms()
        {
            var msg = new SmsMessage
            {
                Message = "my test message",
                Subject = "msg subject",
                DeliveryMethod = DeliveryMethod.Standard,
                PhoneNumbers = new List<string> {"4243876936", "2132212384"},
                StampToSend = DateTime.Now.AddMinutes(5)
            };

            var response = Client.MessagingApi.Send(msg);
            Console.WriteLine("send sms message response: " + response);
        }

        [Test]
        public void SendMms()
        {
            var msg = new MmsMessage
            {
                Message = "my mms message",
                Subject = "msg subject",
                FileId = 22310,
                PhoneNumbers = new List<string> {"4243876936", "2132212384"},
                StampToSend = DateTime.Now.AddMinutes(5)
            };

            var response = Client.MessagingApi.Send(msg);
            Console.WriteLine("send mms message response: " + response);
        }

        [Test]
        public void SendVoice()
        {
            var msg = new VoiceMessage
            {
                Name = "voice broadcast",
                CallerPhoneNumber = "2132212384",
                VoiceSource = "https://eztxting.s3.amazonaws.com/188814/mms/train_1449507791.mp3",
                PhoneNumbers = new List<string> {"2132212384", "2132212384"},
                StampToSend = DateTime.Now.AddMinutes(5)
            };

            var response = Client.MessagingApi.Send(msg);
            Console.WriteLine("send voice message response: " + response);
        }

        [Test]
        public void GetReport()
        {
            var report = Client.MessagingApi.GetReport(59349882);
            Console.WriteLine("getReport response: " + report);
        }

        [Test]
        public void GetDetailedReport()
        {
            var report = Client.MessagingApi.GetDetailedReport(59349882, DeliveryStatus.Delivered);
            Console.WriteLine("getDetailedReport response:");
            report.ToList().ForEach(item => Console.WriteLine(item));
        }
    }
}