using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Utility;

namespace SolrExpress.Solr5.Search.Parameter
{
    [AllowMultipleInstances]
    public sealed class FilterParameter<TDocument> : BaseFilterParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JToken _result;

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
