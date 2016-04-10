namespace EzTextingApiClient.Tests.Integration
{
    public class AbstractIntegrationTest
    {
        protected EzTextingClient Client;

        public AbstractIntegrationTest()
        {
            Client = new EzTextingClient("", "");
        }
    }
}