using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace SolrExpress.Core
{
    public class CustomContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var solrFieldAttribute = member.GetCustomAttribute<SolrFieldAttribute>();

            var property = base.CreateProperty(member, memberSerialization);

            if (solrFieldAttribute != null)
            {
                property.PropertyName = solrFieldAttribute.Name;
            }

            return property;
        }
    }
}
