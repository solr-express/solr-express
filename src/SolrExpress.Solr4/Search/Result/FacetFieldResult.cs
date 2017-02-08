using SolrExpress.Search.Result;
using System.Collections.Generic;
using SolrExpress.Search.Parameter;
using System;

namespace SolrExpress.Solr4.Search.Result
{
    public class FacetFieldResult<TDocument> : IFacetFieldResult<TDocument>
        where TDocument : IDocument
    {
        IEnumerable<FacetKeyValue<string>> IFacetFieldResult<TDocument>.Data { get; set; }

        void ISearchResult.Execute(List<ISearchParameter> searchParameters, string jsonPlainText)
        {
            throw new NotImplementedException();
        }
    }
}
