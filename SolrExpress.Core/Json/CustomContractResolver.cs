using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SolrExpress.Core.Attribute;
using System.Reflection;

namespace SolrExpress.Core.Json
{
    public class CustomContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var solrFieldAttribute = member.GetCustomAttribute<SolrFieldAttribute>();

            var property = base.CreateProperty(member, memberSerialization);

            if (solrFieldAttribute != null)
            {
                property.PropertyName = solrFieldAttribute.Label;
            }

            return property;
        }
    }
}
