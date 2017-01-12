using SolrExpress.Core.Search;
using System.Collections.Generic;

namespace SolrExpress.Core
{
    /// <summary>
    /// Options to control SOLR Query behavior
    /// </summary>
    public class DocumentCollectionOptions
    {
        /// <summary>
        /// If true, check for possibles fails in the use of the Solr Queriable (using SolrFieldAttribute), otherwise false. Default is true
        /// </summary>
        public bool FailFast { get; set; } = true;

        /// <summary>
        /// If true, check for possibles misstakes in use of IANyParameter
        /// </summary>
        public bool CheckAnyParameter { get; set; } = true;

        /// <summary>
        /// Global query interceptor used in all queryable intance
        /// </summary>
        public List<ISearchInterceptor> GlobalQueryInterceptors { get; private set; } = new List<ISearchInterceptor>();

        /// <summary>
        /// Global result interceptor used in all queryable intance
        /// </summary>
        public List<IResultInterceptor> GlobalResultInterceptors { get; private set; } = new List<IResultInterceptor>();
    }

    /// <summary>
    /// Options to control SOLR Query behavior
    /// </summary>
    public class DocumentCollectionOptions<TDocument> : DocumentCollectionOptions
        where TDocument : IDocument
    {
        /// <summary>
        /// SOLR host address
        /// </summary>
        public string HostAddress { get; set; }

        /// <summary>
        /// Global parameter used in all queryable intance
        /// </summary>
        public List<ISearchParameter> GlobalParameters { get; private set; } = new List<ISearchParameter>();
    }
}
