using SolrExpress.Search.Result;
using System.Collections.Generic;
using SolrExpress.Search.Parameter;
using System;

namespace SolrExpress.Solr5.Search.Result
{
    public class FacetRangeResult<TDocument> : IFacetRangeResult<TDocument>
        where TDocument : IDocument
    {
        IEnumerable<FacetKeyValue<FacetRange>> IFacetRangeResult<TDocument>.Data { get; set; }

        void ISearchResult.Execute(List<ISearchParameter> searchParameters, string jsonPlainText)
        {
            throw new NotImplementedException();
        }
    }
}
