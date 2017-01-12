using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class QueryParameter<TDocument> : BaseQueryParameter<TDocument>, ISearchParameterExecute<List<string>>
        where TDocument : IDocument
    {
        public QueryParameter(IExpressionBuilder<TDocument> expressionBuilder)
            : base(expressionBuilder)
        {
        }

        /// <summary>
        /// Execute the creation of the parameter "limit"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            container.Add($"q={this.Value.Execute()}");
        }
    }
}
