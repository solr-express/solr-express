using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class FilterParameter<TDocument> : BaseFilterParameter<TDocument>, ISearchParameter<JObject>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jArray = (JArray)jObject["filter"] ?? new JArray();

            jArray.Add(ExpressionUtility.GetSolrFilterWithTag(this.Value.Execute(), this.TagName));

            jObject["filter"] = jArray;
        }
    }
}
