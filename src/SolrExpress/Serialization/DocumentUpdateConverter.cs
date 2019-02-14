using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolrExpress.Configuration;
using SolrExpress.Update;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SolrExpress.Serialization
{
    public class DocumentUpdateConverter<TDocument> : JsonConverter
        where TDocument : Document
    {
        private readonly SolrDocumentConfiguration<TDocument> _configuration;

        public DocumentUpdateConverter(SolrDocumentConfiguration<TDocument> configuration)
        {
            this._configuration = configuration;
        }

        private string GetFieldName(Expression fieldExpression)
        {
            var propertyInfo = ExpressionUtil.GetPropertyInfoFromExpression(fieldExpression);

#if NET40
            var solrFieldAttribute = (SolrFieldAttribute)member.GetCustomAttributes(typeof(SolrFieldAttribute), false).ToArray().First();
#else
            var solrFieldAttribute = propertyInfo.GetCustomAttribute<SolrFieldAttribute>();
#endif

            if (solrFieldAttribute != null)
            {
                return solrFieldAttribute.Name;
            }
            else
            {
                var solrFieldConfigurations = this._configuration.GetSolrFieldConfigurationList();

                var solrFieldConfiguration = solrFieldConfigurations
                    .FirstOrDefault(q =>
                    {
                        var propertyInfoFromConfiguration = ExpressionUtil.GetPropertyInfoFromExpression(q.FieldExpression);

                        return propertyInfoFromConfiguration.Name.Equals(propertyInfo.Name);
                    });

                // ReSharper disable once InvertIf
                if (solrFieldConfiguration != null)
                {
                    return solrFieldConfiguration.Name;
                }
            }

            throw new ArgumentNullException(nameof(fieldExpression));
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DocumentUpdate<TDocument>);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var documentUpdate = value as DocumentUpdate<TDocument>;

            var jornal = documentUpdate.GetJornal();

            var obj = new Dictionary<string, object>
            {
                { nameof(documentUpdate.Id).ToLowerInvariant(), documentUpdate.Id }
            };

            foreach (var item in jornal)
            {
                var fieldName = this.GetFieldName(item.Key);
                obj.Add(fieldName, item.Value);
            }

            var rawValue = JObject.FromObject(obj, serializer).ToString();

            writer.WriteRaw(rawValue);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
