using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using SolrExpress.Utility;
using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    public sealed class SearchResultBuilder<TDocument>
        where TDocument : IDocument
    {
        private JsonReader _jsonReader;
        private List<ISearchParameter> _searchParameters;
        private List<ISearchResult> _searchResults = new List<ISearchResult>();

        public SearchResultBuilder(
            ISolrExpressServiceProvider<TDocument> serviceProvider)
        {
            Checker.IsNull(serviceProvider);

            this.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Configure parameters and json plain text to be used in builders
        /// </summary>
        /// <param name="searchParameters">Parameters used in search</param>
        /// <param name="jsonPlainText">Result in json plain text</param>
        internal void Configure(List<ISearchParameter> searchParameters, JsonReader jsonReader)
        {
            Checker.IsNull(searchParameters);
            Checker.IsNull(jsonReader);

            this._searchParameters = searchParameters;
            this._jsonReader = jsonReader;
        }

        /// <summary>
        /// Add a instance of informed type in search chain
        /// </summary>
        /// <typeparam name="T">Concrete class that implements the ISearchResult interface</typeparam>
        /// <returns>Instance of T ready to be used</returns>
        public SearchResultBuilder<TDocument> Add<T>(T result)
            where T : ISearchResult
        {
            this._searchResults.Add(result);

            return this;
        }

        /// <summary>
        /// Execute chain of search results
        /// </summary>
        public void Execute()
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
        }

        /// <summary>
        /// Services provider
        /// </summary>
        public ISolrExpressServiceProvider<TDocument> ServiceProvider { get; set; }
    }
}
