using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
#if NET40
using System.Linq;
#endif

namespace SolrExpress.Core
{
    public class CustomContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
#if NET40
            var solrFieldAttribute = (SolrFieldAttribute)member.GetCustomAttributes(typeof(SolrFieldAttribute), false).ToArray().First();
#else
            var solrFieldAttribute = member.GetCustomAttribute<SolrFieldAttribute>();
#endif
            var property = base.CreateProperty(member, memberSerialization);

            if (solrFieldAttribute != null)
            {
                property.PropertyName = solrFieldAttribute.Name;
            }

            return property;
        }
    }
}
