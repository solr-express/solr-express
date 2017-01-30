using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System.Collections.Generic;
using System;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class OffsetParameter<TDocument> : IOffsetParameter<TDocument>, ISearchItemExecution<List<string>>
        where TDocument : IDocument
    {
        bool ISearchParameter.AllowMultipleInstances { get; set; }

        long IOffsetParameter<TDocument>.Value { get; set; }

        void ISearchItemExecution<List<string>>.AddResultInContainer(List<string> container)
        {
            throw new NotImplementedException();
        }

        void ISearchItemExecution<List<string>>.Execute()
        {
            throw new NotImplementedException();
        }
    }
}
