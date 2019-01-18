using Newtonsoft.Json;
using System;

namespace SolrExpress.Serialization
{
    internal class DateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return
                (objectType == typeof(DateTime)) ||
                (objectType == typeof(DateTime?));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime dateTime)
            {
                writer.WriteValue(dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));
            }
            else
            {
                var nullableDateTime = (DateTime?)value;
                if (nullableDateTime.HasValue)
                {
                    writer.WriteValue(nullableDateTime.Value.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));
                }
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            if (reader.Value is DateTime)
            {
                return (DateTime)reader.Value;
            }

            var value = ((string)reader.Value).Replace("T", " ").Replace("Z", string.Empty);

            return DateTime.Parse(value);
        }
    }
}
