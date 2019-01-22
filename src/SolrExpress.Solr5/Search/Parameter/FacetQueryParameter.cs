using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolrExpress.Solr5.Search.Parameter
{
    [AllowMultipleInstances]
    // TODO: Think about this, no implements ISearchItemFieldExpressions<> or ISearchItemFieldExpression<>
    //[FieldMustBeIndexedTrue]
    public sealed class FacetQueryParameter<TDocument> : BaseFacetQueryParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public FacetQueryParameter(ISolrExpressServiceProvider<TDocument> serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public void AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["facet"] ?? new JObject();
            jObj.Add(this._result);
            container["facet"] = jObj;
        }

        public void Execute()
        {
            var array = new List<JProperty>
            {
                new JProperty("q", this.Query.Execute())
            };

            JProperty domain = null;
            if (this.Excludes?.Any() ?? false)
            {
                var excludeValue = new JObject(new JProperty("excludeTags", new JArray(this.Excludes)));
                domain = new JProperty("domain", excludeValue);
            }
            if (this.Filter != null)
            {
                var filter = new JProperty("filter", this.Filter.Execute());
                domain = domain ?? new JProperty("domain", new JObject());
                ((JObject)domain.Value).Add(filter);
            }
            if (domain != null)
            {
                array.Add(domain);
            }

            if (this.Minimum.HasValue)
            {
                array.Add(new JProperty("mincount", this.Minimum.Value));
            }

            if (this.Limit.HasValue)
            {
                array.Add(new JProperty("limit", this.Limit.Value));
            }

            if (this.SortType.HasValue)
            {
                ParameterUtil.GetFacetSort(this.SortType.Value, out string typeName, out string sortName);

                array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));
            }

            if (this.Facets?.Any() ?? false)
            {
                Parallel.ForEach(this.Facets, item => ((ISearchItemExecution<JObject>)item).Execute());

                var subfacets = new JObject();

                foreach (var item in this.Facets)
                {
                    ((ISearchItemExecution<JObject>)item).AddResultInContainer(subfacets);
                }

                array.Add((JProperty)subfacets.First);
            }

            this._result = new JProperty(this.AliasName, new JObject(new JProperty("query", new JObject(array.ToArray()))));
        }
    }
}
