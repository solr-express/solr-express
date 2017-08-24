using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using SolrExpress.Utility;
using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Result builder
    /// </summary>
    internal sealed class SearchResultBuilder<TDocument>
        where TDocument : Document
    {
        private JsonReader _jsonReader;
        private IList<ISearchParameter> _searchParameters;
        private IList<ISearchResult<TDocument>> _searchResults;

        /// <summary>
        /// Configure parameters and json plain text to be used in builders
        /// </summary>
        /// <param name="jsonReader">Result in json reader</param>
        /// <param name="searchParameters">Parameters used in search</param>
        /// <param name="searchResults">Result parsers used in search result</param>
        internal void Configure
        (
            JsonReader jsonReader,
            IList<ISearchParameter> searchParameters,
            IList<ISearchResult<TDocument>> searchResults
        )
        {
            Checker.IsNull(jsonReader);
            Checker.IsNull(searchParameters);
            Checker.IsNull(searchResults);

            this._jsonReader = jsonReader;
            this._searchParameters = searchParameters;
            this._searchResults = searchResults;
        }

        /// <summary>
        /// Execute chain of search results
        /// </summary>
        public IList<ISearchResult<TDocument>> Execute()
        {
            while (this._jsonReader.Read())
            {
                foreach (var searchResult in this._searchResults)
                {
                    searchResult.Execute(
                        this._searchParameters,
                        this._jsonReader.TokenType,
                        this._jsonReader.Path,
                        this._jsonReader);
                }
            }

            return this._searchResults;
        }
    }
}
