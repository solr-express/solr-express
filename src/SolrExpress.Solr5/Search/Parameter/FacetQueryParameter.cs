using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;
using SolrExpress.Solr5.Extension.Internal;
using System.Collections.Generic;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class FacetQueryParameter<TDocument> : BaseFacetQueryParameter<TDocument>, ISearchParameter<JObject>
         where TDocument : IDocument
    {
        /// <summary>
        /// Execute the creation of the parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var facetObject = (JObject)jObject["facet"] ?? new JObject();

            var array = new List<JProperty>
            {
                new JProperty("q",  this.Excludes.GetSolrFacetWithExcludes(this.Query.Execute()))
            };

            array.Add(new JProperty("mincount", 1));

            if (this.SortType.HasValue)
            {
                string typeName;
                string sortName;

                ExpressionUtility.GetSolrFacetSort(this.SortType.Value, out typeName, out sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            var jProperty = new JProperty(this.AliasName, new JObject(new JProperty("query", new JObject(array.ToArray()))));

            facetObject.Add(jProperty);

            jObject["facet"] = facetObject;
        }
    }
}
