using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class FilterQueryParameter<TDocument> : BaseFilterParameter<TDocument>, ISearchParameterExecute<List<string>>
        where TDocument : IDocument
    {
        public FilterQueryParameter(IExpressionBuilder<TDocument> expressionBuilder)
            : base(expressionBuilder)
        {
        }

        /// <summary>
        /// Execute the creation of the parameter "fq"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            var expression = ExpressionUtility.GetSolrFilterWithTag(this.Value.Execute(), this.TagName);

            container.Add($"fq={expression}");
        }
    }
}
