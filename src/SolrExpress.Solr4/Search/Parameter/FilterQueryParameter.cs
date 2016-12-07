using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class FilterQueryParameter<TDocument> : BaseFilterParameter<TDocument>, ISearchParameter<List<string>>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the creation of the parameter "fq"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            this.Value.ExpressionBuilder = this.ExpressionBuilder;

            var expression = ExpressionUtility.GetSolrFilterWithTag(this.Value.Execute(), this.TagName);

            container.Add($"fq={expression}");
        }
    }
}
