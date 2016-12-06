using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class SpatialFilterParameter<TDocument> : BaseSpatialFilterParameter<TDocument>, ISearchParameter<JObject>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var fieldName = ExpressionUtility.GetFieldNameFromExpression(this.Expression);

            var jObj = (JObject)jObject["params"] ?? new JObject();

            var formule = ExpressionUtility.GetSolrSpatialFormule(
                this.FunctionType,
                fieldName,
                this.CenterPoint,
                this.Distance);

            jObj.Add(new JProperty("fq", formule));

            jObject["params"] = jObj;
        }
    }
}
