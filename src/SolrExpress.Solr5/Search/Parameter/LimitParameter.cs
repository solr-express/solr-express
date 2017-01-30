using SolrExpress.Search.Parameter;
using Newtonsoft.Json.Linq;
using SolrExpress.Search;
using System;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class LimitParameter<TDocument> : ILimitParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : IDocument
    {
        bool ISearchParameter.AllowMultipleInstances { get; set; }

        long ILimitParameter<TDocument>.Value { get; set; }

        void ISearchItemExecution<JObject>.AddResultInContainer(JObject container)
        {
            throw new NotImplementedException();
        }

        void ISearchItemExecution<JObject>.Execute()
        {
            throw new NotImplementedException();
        }
    }
}
