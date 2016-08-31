using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Extension.Internal;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;
using SolrExpress.Solr5.Extension.Internal;
using System.Collections.Generic;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class FacetRangeParameter<TDocument> : BaseFacetRangeParameter<TDocument>, ISearchParameter<JObject>
      where TDocument : IDocument
    {
        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var facetObject = (JObject)jObject["facet"] ?? new JObject();

            var fieldName = this.Expression.GetFieldNameFromExpression();

            var array = new List<JProperty>
            {
                new JProperty("field", this.Excludes.GetSolrFacetWithExcludes(fieldName))
            };

            array.Add(new JProperty("mincount", 1));

            if (!string.IsNullOrWhiteSpace(this.Gap))
            {
                array.Add(new JProperty("gap", this.Gap));
            }
            if (!string.IsNullOrWhiteSpace(this.Start))
            {
                array.Add(new JProperty("start", this.Start));
            }
            if (!string.IsNullOrWhiteSpace(this.End))
            {
                array.Add(new JProperty("end", this.End));
            }

            array.Add(new JProperty("other", new JArray("before", "after")));

            if (this.SortType.HasValue)
            {
                string typeName;
                string sortName;

                ExpressionUtility.GetSolrFacetSort(this.SortType.Value, out typeName, out sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            var value = new JProperty(this.AliasName, new JObject(new JProperty("range", new JObject(array.ToArray()))));

            facetObject.Add(value);

            jObject["facet"] = facetObject;
        }
    }
}
