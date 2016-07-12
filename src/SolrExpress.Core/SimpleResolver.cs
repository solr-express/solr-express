using System;
using System.Collections.Generic;
using System.Linq;
#if NETCOREAPP1_0
using System.Reflection;
#endif

namespace SolrExpress.Core
{
    /// <summary>
    /// Simple dependency injection resolver
    /// </summary>
    public class SimpleResolver : IResolver
    {
        /// <summary>
        /// Resolve target type based in informed source type
        /// </summary>
        /// <param name="source">Source type used to resolve target type</param>
        /// <returns>Target type or null</returns>
        private Type ResolveType(Type source)
        {
            var item = this
                .Mappings
                .Keys
                .FirstOrDefault(q => $"{q.Namespace}.{q.Name}".Equals($"{source.Namespace}.{source.Name}"));

            if (item == null)
            {
                return null;
            }

            var target = this.Mappings[item];


#if NETCOREAPP1_0
            if (target.GetTypeInfo().ContainsGenericParameters)
#else
            if (target.ContainsGenericParameters)
#endif
            {
#if NET40
                target = target.MakeGenericType(source.GetGenericArguments());
#else
                target = target.MakeGenericType(source.GenericTypeArguments);
#endif
            }

            return target;
        }

        /// <summary>
        /// Get concrete class that implements informed interface
        /// </summary>
        /// <returns>Concrete class</returns>
        public T GetInstance<T>()
        {
            var target = this.ResolveType(typeof(T));

            if (target == null)
            {
                throw new UnexpectedDependencyInjectionMappingException(typeof(T).FullName);
            }

#if NETCOREAPP1_0
            var constructor = target.GetTypeInfo().GetConstructors()[0];
#else
            var constructor = target.GetConstructors()[0];
#endif
            return (T)constructor.Invoke(new object[] { });
        }

        /// <summary>
        /// Mappings to resolve dependency injection
        /// </summary>
        public Dictionary<Type, Type> Mappings { get; private set; } = new Dictionary<Type, Type>();
    }
}
