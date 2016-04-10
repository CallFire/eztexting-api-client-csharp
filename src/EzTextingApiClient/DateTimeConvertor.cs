using Newtonsoft.Json.Converters;

namespace EzTextingApiClient
{
    public class DateTimeConvertor : IsoDateTimeConverter
    {
        public DateTimeConvertor()
        {
            base.DateTimeFormat = ClientConstants.DateFormatPattern;
        }
    }
}