using System;
using System.Collections.Generic;
using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Api.Messaging.Model;
using NUnit.Framework;
using RestSharp;
using RestSharp.Extensions;

namespace EzTextingApiClient.Tests.Api.Messaging
{
    [TestFixture]
    public class MessagingApiTest : AbstractApiTest
    {
        [Test]
        public void SendSms()
        {
            var expectedJson = GetJsonPayload("/messaging/messagingApi/sendSms.json");
            var restRequest = MockRestResponse(expectedJson);

            var now = DateTime.Now;
            var sms = new SmsMessage()
            {
                Subject = "test subject",
                Message = "this is mms message",
                PhoneNumbers = new List<string> {"1234567890", "2345678900", "3456789000"},
                Groups = new List<string> {"group1", "group2", "group3"},
                StampToSend = now
            };
            var response = Client.MessagingApi.Send(sms);
            EzTextingResponse<SmsMessage> ezResponse = new EzTextingResponse<SmsMessage>("Success", 201, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));
            Assert.That(bodyParam.Value, Is.StringContaining("Subject=" + "test subject".UrlEncode()));
            Assert.That(bodyParam.Value, Is.StringContaining("Subject=" + "test subject".UrlEncode()));
            Assert.That(bodyParam.Value, Is.StringContaining("Groups[]=group1"));
            Assert.That(bodyParam.Value, Is.StringContaining("Groups[]=group2"));
            Assert.That(bodyParam.Value, Is.StringContaining("Groups[]=group3"));
            Assert.That(bodyParam.Value, Is.StringContaining("PhoneNumbers[]=1234567890"));
            Assert.That(bodyParam.Value, Is.StringContaining("PhoneNumbers[]=2345678900"));
            Assert.That(bodyParam.Value, Is.StringContaining("PhoneNumbers[]=3456789000"));
            Assert.That(bodyParam.Value, Is.StringContaining("MessageTypeID=1"));
            Assert.That(bodyParam.Value, Is.StringContaining("StampToSend=" + ClientUtils.ToUnixTime(now)/1000));
        }

        [Test]
        public void SendMms()
        {
            var expectedJson = GetJsonPayload("/messaging/messagingApi/sendMms.json");
            var restRequest = MockRestResponse(expectedJson);

            var now = DateTime.Now;
            var mms = new MmsMessage()
            {
                FileId = 123,
                Subject = "Subj",
                StampToSend = now
            };
            var response = Client.MessagingApi.Send(mms);

            EzTextingResponse<MmsMessage> ezResponse = new EzTextingResponse<MmsMessage>("Success", 201, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));
            Assert.That(bodyParam.Value, Is.StringContaining("FileID=123"));
            Assert.That(bodyParam.Value, Is.StringContaining("Subject=Subj"));
            Assert.That(bodyParam.Value, Is.StringContaining("MessageTypeID=3"));
            Assert.That(bodyParam.Value, Is.StringContaining("StampToSend=" + ClientUtils.ToUnixTime(now)/1000));
        }

        [Test]
        public void SendVoice()
        {
            var expectedJson = GetJsonPayload("/messaging/messagingApi/sendVoice.json");
            var restRequest = MockRestResponse(expectedJson);

            var now = DateTime.Now;
            var message = new VoiceMessage()
            {
                CallerPhoneNumber = "1234567890",
                Name = "test broadcast",
                VoiceFile = "voice.wav",
                VoiceSource = "http://yoursite.com/voice.mp3",
                PhoneNumbers = new List<string> {"1234567890", "2345678900", "3456789000"},
                Groups = new List<string> {"group1", "group2", "group3"},
                StampToSend = now
            };

            var response = Client.MessagingApi.Send(message);
            EzTextingResponse<VoiceMessage> ezResponse = new EzTextingResponse<VoiceMessage>("Success", 201, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.IsTrue(restRequest.Value.Parameters.Count > 0);
            var bodyParam = restRequest.Value.Parameters[0];

            Assert.AreEqual(ClientConstants.FormEncodedContentType, bodyParam.ContentType);
            Assert.AreEqual(ParameterType.RequestBody, bodyParam.Type);

            Assert.That(bodyParam.Value, Is.StringContaining("User=login"));
            Assert.That(bodyParam.Value, Is.StringContaining("Password=password"));
            Assert.That(bodyParam.Value, Is.StringContaining("CallerPhonenumber=1234567890"));
            Assert.That(bodyParam.Value, Is.StringContaining("Groups[]=group1"));
            Assert.That(bodyParam.Value, Is.StringContaining("Groups[]=group2"));
            Assert.That(bodyParam.Value, Is.StringContaining("Groups[]=group3"));
            Assert.That(bodyParam.Value, Is.StringContaining("PhoneNumbers[]=1234567890"));
            Assert.That(bodyParam.Value, Is.StringContaining("PhoneNumbers[]=2345678900"));
            Assert.That(bodyParam.Value, Is.StringContaining("PhoneNumbers[]=3456789000"));
            Assert.That(bodyParam.Value, Is.StringContaining("Name=" + "test broadcast".UrlEncode()));
            Assert.That(bodyParam.Value, Is.StringContaining("VoiceFile=voice.wav"));
            Assert.That(bodyParam.Value, Is.StringContaining("VoiceSource="
                                                             + "http://yoursite.com/voice.mp3".UrlEncode()));
            Assert.That(bodyParam.Value, Is.StringContaining("VoiceFile=voice.wav"));
            Assert.That(bodyParam.Value, Is.StringContaining("StampToSend=" + ClientUtils.ToUnixTime(now)/1000));
        }

        [Test]
        public void GetReport()
        {
            var expectedJson = GetJsonPayload("/messaging/messagingApi/getReport.json");
            var restRequest = MockRestResponse(expectedJson);

            var response = Client.MessagingApi.GetReport(11L);
            var ezResponse = new EzTextingResponse<DeliveryReport>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/11"));
        }

        [Test]
        public void GetDetailedReport()
        {
            var expectedJson = GetJsonPayload("/messaging/messagingApi/getDetailedReport.json");
            var restRequest = MockRestResponse(expectedJson);

            var response = Client.MessagingApi.GetDetailedReport(11L, DeliveryStatus.NoCredits);
            EzTextingResponse<long?> ezResponse = new EzTextingResponse<long?>("Success", 200, response);
            Assert.That(Serializer.Serialize(ezResponse), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("User=login"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("Password=password"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/11"));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("status=no_credits"));
        }
    }
}