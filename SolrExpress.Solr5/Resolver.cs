using SolrExpress.Core;
using System;

namespace SolrExpress.Solr5
{
    /// <summary>
    /// Solr 5 classes dependency resolver
    /// </summary>
    public sealed class Resolver : IResolver
    {
        /// <summary>
        /// Get concrete class that implements informed interface
        /// </summary>
        /// <returns>Concrete class</returns>
        public T Get<T>()
        {
            throw new NotImplementedException();
        }
    }
}
