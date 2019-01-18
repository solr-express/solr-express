using Newtonsoft.Json;
using System;
using System.Globalization;

namespace SolrExpress.Serialization
{
    internal class GeoCoordinateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return
                (objectType == typeof(GeoCoordinate)) ||
                (objectType == typeof(GeoCoordinate?));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is GeoCoordinate geoCoordinate)
            {
                writer.WriteValue(geoCoordinate.ToString());
            }
            else
            {
                var nullableGeoCoordinate = (GeoCoordinate?)value;
                if (nullableGeoCoordinate.HasValue)
                {
                    writer.WriteValue(nullableGeoCoordinate.ToString());
                }
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var args = ((string)reader.Value).Split(',');

            var latitude = Convert.ToDecimal(args[0], CultureInfo.InvariantCulture);
            var longitude = Convert.ToDecimal(args[1], CultureInfo.InvariantCulture);

            return new GeoCoordinate(latitude, longitude);
        }
    }
}
