using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr5.Search.Parameter
{
    [AllowMultipleInstances]
    [FieldMustBeIndexedTrue]
    public class FacetQueryParameter<TDocument> : IFacetQueryParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        private JProperty _result;
        
        string IFacetQueryParameter<TDocument>.AliasName { get; set; }

        string[] IFacetQueryParameter<TDocument>.Excludes { get; set; }

        int? IFacetQueryParameter<TDocument>.Limit { get; set; }

        int? IFacetQueryParameter<TDocument>.Minimum { get; set; }

        SearchQuery IFacetQueryParameter<TDocument>.Query { get; set; }

        FacetSortType? IFacetQueryParameter<TDocument>.SortType { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["facet"] ?? new JObject();
            jObj.Add(this._result);
            container["facet"] = jObj;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IFacetQueryParameter<TDocument>)this;

            var array = new List<JProperty>
            {
                new JProperty("q", parameter.Query.Execute())
            };

            if (parameter.Excludes?.Any() ?? false)
            {
                var excludeValue = new JObject(new JProperty("excludeTags", new JArray(parameter.Excludes)));
                array.Add(new JProperty("domain", excludeValue));
            }

            if (parameter.Minimum.HasValue)
            {
                array.Add(new JProperty("mincount", parameter.Minimum.Value));
            }

            if (parameter.Limit.HasValue)
            {
                array.Add(new JProperty("limit", parameter.Limit.Value));
            }

            if (parameter.SortType.HasValue)
            {
                string typeName;
                string sortName;

                ParameterUtil.GetFacetSort(parameter.SortType.Value, out typeName, out sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            this._result = new JProperty(parameter.AliasName, new JObject(new JProperty("query", new JObject(array.ToArray()))));
        }
    }
}
