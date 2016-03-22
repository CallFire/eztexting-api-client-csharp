namespace EzTextingApiClient.Api.Messaging.Model
{
    public class SmsMessage : TextMessage
    {
        public SmsMessage()
        {
            DeliveryMethod = DeliveryMethod.Express;
        }
    }
}