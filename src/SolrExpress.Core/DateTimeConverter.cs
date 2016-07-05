using Newtonsoft.Json;
using System;

namespace SolrExpress.Core
{
    internal class DateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(DateTime));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value is DateTime)
            {
                return (DateTime)reader.Value;
            }

            var value = ((string)reader.Value).Replace("T", " ").Replace("Z", string.Empty);

            return DateTime.Parse(value);
        }
    }
}
