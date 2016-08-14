using SolrExpress.Core.Query;
using SolrExpress.Core.Update;

namespace SolrExpress.Core
{
    /// <summary>
    /// Signatures to SOLR document collection
    /// </summary>
    public interface IDocumentCollection<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Solr queryable instance to provide create queries in SOLR
        /// </summary>
        ISolrQueryable<TDocument> Select();

        /// <summary>
        /// Solr queryable instance to provide create queries in SOLR
        /// </summary>
        ISolrAtomicUpdate<TDocument> Update();

        /// <summary>
        /// Provider used to resolve the expression
        /// </summary>
        IProvider Provider { get; }

        /// <summary>
        /// Resolver used to resolve classes dependency
        /// </summary>
        IResolver Resolver { get; }

        /// <summary>
        /// Configurations about SolrQueriable behavior
        /// </summary>
        Configuration Configuration { get; }
    }
}
