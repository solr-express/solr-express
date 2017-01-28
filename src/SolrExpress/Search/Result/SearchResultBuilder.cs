using SolrExpress.Search.Parameter;
using SolrExpress.Utility;
using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    public sealed class SearchResultBuilder<TDocument>
        where TDocument : IDocument
    {
        private string _jsonPlainText;
        private List<ISearchParameter> _searchParameters;

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
        internal void Configure(List<ISearchParameter> searchParameters, string jsonPlainText)
        {
            Checker.IsNull(searchParameters);
            Checker.IsNullOrWhiteSpace(jsonPlainText);

            this._searchParameters = searchParameters;
            this._jsonPlainText = jsonPlainText;
        }

        /// <summary>
        /// Get a instance of the informed type with search
        /// </summary>
        /// <typeparam name="T">Concrete class that implements the ISearchResult interface</typeparam>
        /// <returns>Instance of T ready to be used</returns>
        public T Get<T>(T result)
            where T : ISearchResult
        {
            result.Execute(this._searchParameters, this._jsonPlainText);

            return result;
        }

        /// <summary>
        /// Services provider
        /// </summary>
        public ISolrExpressServiceProvider<TDocument> ServiceProvider { get; set; }
    }
}
