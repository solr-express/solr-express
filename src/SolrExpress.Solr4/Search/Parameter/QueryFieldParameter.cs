using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class QueryFieldParameter<TDocument> : BaseQueryFieldParameter<TDocument>, ISearchParameterExecute<List<string>>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the creation of the parameter "query field"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            container.Add($"qf={this.Expression}");
        }
    }
}
