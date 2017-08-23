using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolrExpress.Solr5.Search.Parameter
{
    [AllowMultipleInstances]
    // TODO: Think about this, no implements ISearchItemFieldExpressions<> or ISearchItemFieldExpression<>
    //[FieldMustBeIndexedTrue]
    public class FacetQueryParameter<TDocument> : IFacetQueryParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public FacetQueryParameter(ISolrExpressServiceProvider<TDocument> serviceProvider)
        {
            ((IFacetParameter<TDocument>)this).ServiceProvider = serviceProvider;
        }

        string IFacetQueryParameter<TDocument>.AliasName { get; set; }

        string[] IFacetQueryParameter<TDocument>.Excludes { get; set; }

        int? IFacetQueryParameter<TDocument>.Limit { get; set; }

        int? IFacetQueryParameter<TDocument>.Minimum { get; set; }

        SearchQuery<TDocument> IFacetQueryParameter<TDocument>.Query { get; set; }

        FacetSortType? IFacetQueryParameter<TDocument>.SortType { get; set; }

        ISolrExpressServiceProvider<TDocument> IFacetParameter<TDocument>.ServiceProvider { get; set; }

        IList<IFacetParameter<TDocument>> IFacetParameter<TDocument>.Facets { get; set; }
        
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

            if (parameter.Facets?.Any() ?? false)
            {
                Parallel.ForEach(parameter.Facets, item => ((ISearchItemExecution<JObject>)item).Execute());

                var subfacets = new JObject();

                foreach (var item in parameter.Facets)
                {
                    ((ISearchItemExecution<JObject>)item).AddResultInContainer(subfacets);
                }

                array.Add((JProperty)subfacets.First);
            }

            this._result = new JProperty(parameter.AliasName, new JObject(new JProperty("query", new JObject(array.ToArray()))));
        }
    }
}
