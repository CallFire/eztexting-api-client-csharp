using System;
using NUnit.Framework;

namespace EzTextingApiClient.Tests.Integration
{
    [TestFixture, Ignore("temporary disabled")]
    public class ToolboxIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void CarrierLookup()
        {
            var response = Client.ToolboxApi.CarrierLookup("4243876936");
            Console.WriteLine("lookup response: " + response);
        }
    }
}