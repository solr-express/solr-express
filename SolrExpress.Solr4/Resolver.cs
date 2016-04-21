using SolrExpress.Core;
using SolrExpress.Core.Result;
using SolrExpress.Solr4.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr4
{
    /// <summary>
    /// Solr 4 classes dependency resolver
    /// </summary>
    public sealed class Resolver : IResolver
    {
        private Dictionary<Type, Type> mappings = new Dictionary<Type, Type>();

        private Type ResolveType(Type type)
        {
            if (mappings.Keys.Contains(type))
            {
                return mappings[type];
            }

            return null;
        }

        /// <summary>
        /// Get concrete class that implements informed interface
        /// </summary>
        /// <returns>Concrete class</returns>
        public T Get<T>()
        {
            this.mappings.Add(typeof(IDocumentResult<>), typeof(DocumentResult<>));

            var target = ResolveType(typeof(T));
            var constructor = target.GetConstructors()[0];
            return (T)constructor.Invoke(new object[] { });
        }

        //public IDocumentBuilder<TDocument> GetDocumentBuilder()
        //return new DocumentBuilder<TDocument>();

        //public IFacetFieldResultBuilder<TDocument> GetFacetFieldBuilder()
        //return new FacetFieldResultBuilder<TDocument>();

        //public IFacetQueryResultBuilder<TDocument> GetFacetQueryBuilder()
        //return new FacetQueryResultBuilder<TDocument>();

        //public IFacetRangeResultBuilder<TDocument> GetFacetRangeBuilder()
        //return new FacetRangeResultBuilder<TDocument>();

        //public IStatisticResultBuilder<TDocument> GetStatisticBuilder()
        //return new StatisticResultBuilder<TDocument>();


    }
}
