﻿using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Query;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class StandardQueryParameter<TDocument> : IStandardQueryParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        SearchQuery IStandardQueryParameter<TDocument>.Value { get;set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            var parameter = (IStandardQueryParameter<TDocument>)this;
            this._result = new JProperty("q.alt", parameter.Value.Execute());
        }
    }
}