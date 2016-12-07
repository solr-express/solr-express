using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class QueryParameter<TDocument> : BaseQueryParameter<TDocument>, ISearchParameter<List<string>>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the creation of the parameter "limit"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            this.Value.ExpressionBuilder = this.ExpressionBuilder;

            container.Add($"q={this.Value.Execute()}");
        }
    }
}
