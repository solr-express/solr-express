using SolrExpress.Core;
using System;

namespace SolrExpress.Solr5
{
    /// <summary>
    /// Solr 5 classes dependency resolver
    /// </summary>
    public sealed class Resolver : IResolver
    {
        public TConcrete Get<TConcrete>() where TConcrete : class
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get concrete class that implements informed interface
        /// </summary>
        /// <returns>Concrete class</returns>
        public T GetParameter<T>()
            where T : class
        {
            throw new NotImplementedException();
        }
    }
}
