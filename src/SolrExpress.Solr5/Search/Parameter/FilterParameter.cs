using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Utility;

namespace SolrExpress.Solr5.Search.Parameter
{
    [AllowMultipleInstances]
    public sealed class FilterParameter<TDocument> : IFilterParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JToken _result;

        SearchQuery<TDocument> IFilterParameter<TDocument>.Query { get; set; }

        string IFilterParameter<TDocument>.TagName { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jArray = (JArray)container["filter"] ?? new JArray();
            jArray.Add(this._result);
            container["filter"] = jArray;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IFilterParameter<TDocument>)this;
            this._result = ParameterUtil.GetFilterWithTag(parameter.Query.Execute(), parameter.TagName);
        }
    }
}
