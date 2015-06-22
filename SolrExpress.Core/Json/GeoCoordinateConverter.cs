using Newtonsoft.Json;
using SolrExpress.Core.Query;
using System;
using SolrExpress.Core.Entity;

namespace SolrExpress.Core.Json
{
    internal class GeoCoordinateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(GeoCoordinate));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((GeoCoordinate)value).ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var args = ((string)reader.Value).Split(',');

            var latitude = Convert.ToDecimal(args[0]);
            var longitude = Convert.ToDecimal(args[1]);

            return new GeoCoordinate(latitude, longitude);
        }
    }
}
