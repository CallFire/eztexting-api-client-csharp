using System;
using EzTextingApiClient.Api.Toolbox.Model;
using Newtonsoft.Json;

namespace EzTextingApiClient
{
    public class CarrierNameConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((CarrierName) value).EnumMemberAttr<CarrierName>());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var enumObj = ClientUtils.EnumFromString<CarrierName>(reader.Value.ToString());
            return reader.Value == null ? (object) null : enumObj;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CarrierName);
        }
    }
}