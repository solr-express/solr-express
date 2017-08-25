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

        public SearchQuery<TDocument> Query { get; set; }
        public string TagName { get; set; }

        public void AddResultInContainer(JObject container)
        {
            var jArray = (JArray)container["filter"] ?? new JArray();
            jArray.Add(this._result);
            container["filter"] = jArray;
        }

        public void Execute()
        {
            this._result = ParameterUtil.GetFilterWithTag(this.Query.Execute(), this.TagName);
        }
    }
}
