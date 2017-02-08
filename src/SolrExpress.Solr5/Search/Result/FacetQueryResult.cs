using SolrExpress.Search.Result;
using System.Collections.Generic;
using SolrExpress.Search.Parameter;
using System;

namespace SolrExpress.Solr5.Search.Result
{
    public class FacetQueryResult<TDocument> : IFacetQueryResult<TDocument>
        where TDocument : IDocument
    {
        IDictionary<string, long> IFacetQueryResult<TDocument>.Data { get; set; }

        object IFacetQueryResult<TDocument>.Tag { get; set; }

        void ISearchResult.Execute(List<ISearchParameter> searchParameters, string jsonPlainText)
        {
            throw new NotImplementedException();
        }
    }
}
