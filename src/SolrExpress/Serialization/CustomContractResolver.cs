using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SolrExpress.Configuration;
using SolrExpress.Utility;
using System.Linq;
using System.Reflection;
#if NET40
using System.Linq;
#endif

namespace SolrExpress.Serialization
{
    public class CustomContractResolver<TDocument> : DefaultContractResolver
        where TDocument : Document
    {
        private readonly SolrDocumentConfiguration<TDocument> _configuration;

        public CustomContractResolver(SolrDocumentConfiguration<TDocument> configuration)
        {
            this._configuration = configuration;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
#if NET40
            var solrFieldAttribute = (SolrFieldAttribute)member.GetCustomAttributes(typeof(SolrFieldAttribute), false).ToArray().First();
#else
            var solrFieldAttribute = member.GetCustomAttribute<SolrFieldAttribute>();
#endif
            var property = base.CreateProperty(member, memberSerialization);

            // ReSharper disable once InvertIf
            if (solrFieldAttribute != null)
            {
                property.PropertyName = solrFieldAttribute.Name;
                property.Ignored = solrFieldAttribute.IsMagicField;
            }
            else
            {
                var solrFieldConfigurations = this._configuration.GetSolrFieldConfigurationList();

                var solrFieldConfiguration = solrFieldConfigurations
                    .FirstOrDefault(q =>
                    {
                        var propertyInfo = ExpressionUtil.GetPropertyInfoFromExpression(q.FieldExpression);

                        return property.PropertyName.Equals(propertyInfo.Name);
                    });

                // ReSharper disable once InvertIf
                if (solrFieldConfiguration != null)
                {
                    property.PropertyName = solrFieldConfiguration.Name;
                    property.Ignored = solrFieldConfiguration.IsMagicField;
                }
            }

            return property;
        }
    }
}
