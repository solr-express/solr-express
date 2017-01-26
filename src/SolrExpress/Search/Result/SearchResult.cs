using System.Collections.Generic;
using SolrExpress.Search.Parameter;

namespace SolrExpress.Search.Result
{
    public sealed class SearchResult<TDocument>
        where TDocument : IDocument
    {
        private string _json;
        private List<ISearchParameter> _searchParameters;

        public SearchResult(List<ISearchParameter> searchParameters, string json)
        {
            this._searchParameters = searchParameters;
            this._json = json;
        }
    }
}
