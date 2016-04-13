using SolrExpress.Core;
using System;

namespace SolrExpress.Solr4
{
    /// <summary>
    /// Solr 4 classes dependency resolver
    /// </summary>
    public sealed class Resolver : IResolver
    {
        /// <summary>
        /// Get concrete class that implements informed interface
        /// </summary>
        /// <returns>Concrete class</returns>
        public T Get<T>() 
            where T : class
        {
            throw new NotImplementedException();

//            var dic = new Dictionary<Type, object> {
//                []
//            };





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
}
