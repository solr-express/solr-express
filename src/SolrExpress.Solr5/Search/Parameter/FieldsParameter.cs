using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class FieldsParameter<TDocument> : BaseFieldsParameter<TDocument>, ISearchParameter<JObject>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jArray = (JArray)jObject["fields"] ?? new JArray();

            foreach (var expression in this.Expressions)
            {
                var value = expression.GetFieldNameFromExpression();

                jArray.Add(value);
            }

            jObject["fields"] = jArray;
        }
    }
}
