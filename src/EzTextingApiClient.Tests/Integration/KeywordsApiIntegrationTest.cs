using System;
using EzTextingApiClient.Api.Credits.Model;
using EzTextingApiClient.Api.Keywords.Model;
using EzTextingApiClient.Api.Messaging.Model;
using NUnit.Framework;

namespace EzTextingApiClient.Tests.Integration
{
    [TestFixture, Ignore("temporary disabled")]
    public class KeywordsApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void CheckAvailability()
        {
            Console.WriteLine("EZAYW69417: " + Client.KeywordsApi.CheckAvailability("EZAYW69417"));
            Console.WriteLine("Superman: " + Client.KeywordsApi.CheckAvailability("HELP"));
        }

        [Test]
        public void Setup()
        {
            var keyword = new Keyword
            {
                Value = "EZUYO11939",
                ConfirmMessage = new SimpleMessage
                {
                    DeliveryMethod = DeliveryMethod.Express,
                    Subject = "subj",
                    Message = "conf updated"
                }
            };
            var updated = Client.KeywordsApi.Setup(keyword);
            Console.WriteLine("updated keyword: " + updated);
        }

        [Test]
        public void RentWithNewCreditCard()
        {
            var cc = new CreditCard
            {
                FirstName = "John",
                LastName = "Doe",
                State = "LA",
                SecurityCode = "123",
                ExpirationMonth = "11",
                ExpirationYear = "2020",
                Number = "4111111111111111",
                City = "LA",
                Country = "US",
                Street = "1 Avenue",
                Zip = "31331",
                Type = CreditCardType.Visa
            };
            var kw = Client.KeywordsApi.Rent("MYKWE", cc);
            Console.WriteLine("rent kw new cc: " + kw);
        }

        [Test]
        public void RentWithStoredCreditCard()
        {
            var kw = Client.KeywordsApi.Rent("MYKWS", "1111");
            Console.WriteLine("rent kw existing cc: " + kw);
        }

        [Test]
        public void Cancel()
        {
            Client.KeywordsApi.Cancel("EZAYW69417");
            var ex = Assert.Throws<EzTextingApiException>(() => Client.KeywordsApi.Cancel("EZAYW69417"));
            Assert.That(ex.Message, Is.EqualTo("Sorry, nothing was found"));
        }
    }
}