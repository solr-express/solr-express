using SolrExpress.Search.Behaviour;
using SolrExpress.Search.Interceptor;
using SolrExpress.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Options
{
    /// <summary>
    /// Options to control SolrExpress behavior
    /// </summary>
    public class SolrExpressOptions
    {
        /// <summary>
        /// If true, check for possibles fails in the use of the Solr Queriable (using SolrFieldAttribute), otherwise false. Default is true
        /// </summary>
        public bool FailFast { get; set; } = true;

        /// <summary>
        /// If true, add default query parser (Edismax) in all searchs
        /// </summary>
        public bool SetQueryParser { get; set; } = true;

        /// <summary>
        /// If true, add default standard query (q.alt) in all searchs
        /// </summary>
        public bool SetStandardQuery { get; set; } = true;

        /// <summary>
        /// If true, add default field (df) in all searchs
        /// </summary>
        public bool SetDefaultField { get; set; } = true;

        /// <summary>
        /// If true, check for possibles mistakes in use of IANyParameter
        /// </summary>
        public bool CheckAnyParameter { get; set; } = true;

        /// <summary>
        /// SOLR host address
        /// </summary>
        public string HostAddress { get; set; }

        /// <summary>
        /// Options to security connection
        /// </summary>
        public SecurityOptions Security { get; set; } = new SecurityOptions();

        /// <summary>
        /// Global prefix name in dynamic field configurations
        /// </summary>
        public string GlobalDynamicFieldPrefix { get; set; }

        /// <summary>
        /// Global suffix name in dynamic field configurations
        /// </summary>
        public string GlobalDynamicFieldSuffix { get; set; }

        /// <summary>
        /// Global result interceptor used in all queryable intance
        /// </summary>
        public List<IResultInterceptor> GlobalResultInterceptors { get; set; } = new List<IResultInterceptor>();

        /// <summary>
        /// Global parameter used in all queryable intance
        /// </summary>
        public List<ISearchParameter> GlobalParameters { get; set; } = new List<ISearchParameter>();

        /// <summary>
        /// Global change behaviour used in all queryable intance
        /// </summary>
        public List<IChangeBehaviour> GlobalChangeBehaviours { get; set; } = new List<IChangeBehaviour>();
    }
}
