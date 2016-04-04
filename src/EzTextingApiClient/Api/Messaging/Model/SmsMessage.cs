namespace EzTextingApiClient.Api.Messaging.Model
{
    public class SmsMessage : TextMessage
    {
        public SmsMessage()
        {
            DeliveryMethod = DeliveryMethod.Express;
        }

        public override string ToString() =>
            $"SmsMessage [{base.ToString()}]";
    }
}