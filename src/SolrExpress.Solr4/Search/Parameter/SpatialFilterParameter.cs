using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class SpatialFilterParameter<TDocument> : BaseSpatialFilterParameter<TDocument>, ISearchParameter<List<string>>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            var fieldName = ExpressionUtility.GetFieldNameFromExpression(this.Expression);

            var formule = ExpressionUtility.GetSolrSpatialFormule(this.FunctionType, fieldName, this.CenterPoint, this.Distance);

            container.Add($"fq={formule}");
        }
    }
}
