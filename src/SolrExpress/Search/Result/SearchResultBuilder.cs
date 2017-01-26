using SolrExpress.Search.Parameter;
using SolrExpress.Utility;
using System.Collections.Generic;

namespace SolrExpress.Search.Result
{
    public sealed class SearchResultBuilder<TDocument>
        where TDocument : IDocument
    {
        private readonly string _jsonPlainText;
        private readonly List<ISearchParameter> _searchParameters;

        public SearchResultBuilder(List<ISearchParameter> searchParameters, string jsonPlainText)
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
    }
}
