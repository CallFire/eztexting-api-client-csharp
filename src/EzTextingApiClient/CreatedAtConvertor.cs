using Newtonsoft.Json.Converters;

namespace EzTextingApiClient
{
    public class CreatedAtConvertor : IsoDateTimeConverter
    {
        public CreatedAtConvertor()
        {
            base.DateTimeFormat = ClientConstants.CreatedAtFormatPattern;
        }
    }
}