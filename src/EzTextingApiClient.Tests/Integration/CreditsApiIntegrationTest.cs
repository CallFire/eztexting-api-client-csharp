using System;
using System.Runtime.InteropServices;
using EzTextingApiClient.Api.Credits.Model;
using NUnit.Framework;

namespace EzTextingApiClient.Tests.Integration
{
    [TestFixture, Ignore("temporary disabled")]
    public class CreditsApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void CheckBalance()
        {
            var balance = Client.CreditsApi.CheckBalance();
            Console.WriteLine("balance: " + balance);
        }

        [Test]
        public void BuyCreditsUsingStoredCreditCard()
        {
            var request = new BuyCreditsRequest
            {
                Credits = 2000,
                StoredCard = "1111"
            };
            var response = Client.CreditsApi.BuyCredits(request);
            Console.WriteLine("buy credits with stored card response: " + response);
        }

        [Test]
        public void BuyCreditsUsingNewCreditCard()
        {
            var request = new BuyCreditsRequest
            {
                Credits = 2000,
                CouponCode = "ABX32WE",
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
            var response = Client.CreditsApi.BuyCredits(request);
            Console.WriteLine("buy credits with new card response: " + response);
        }
    }
}