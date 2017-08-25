using SolrExpress.Options;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Extension;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Search.Result;
using SolrExpress.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

//TODO: Add unit tests
namespace SolrExpress.Search
{
    /// <summary>
    /// Document search engine
    /// </summary>
    public class DocumentSearch<TDocument>
        where TDocument : Document
    {
        private readonly SolrExpressOptions _solrExpressOptions;
        private readonly ISearchItemCollection<TDocument> _searchItemCollection;
        private string _requestHandler = RequestHandler.Select;
        internal ISolrExpressServiceProvider<TDocument> ServiceProvider;

        public DocumentSearch(
            SolrExpressOptions solrExpressOptions,
            ISolrExpressServiceProvider<TDocument> serviceProvider,
            ISearchItemCollection<TDocument> searchItemCollection)
        {
            Checker.IsNull(solrExpressOptions);
            Checker.IsNull(serviceProvider);
            Checker.IsNull(searchItemCollection);

            this._solrExpressOptions = solrExpressOptions;
            this._searchItemCollection = searchItemCollection;
            this.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Execute search item validation
        /// </summary>
        /// <param name="item">Search item to validate</param>
        private void ValidateSearchItem(ISearchItem item)
        {
            var searchParameter = item as ISearchParameter;

            if (!this._solrExpressOptions.FailFast || searchParameter == null)
            {
                return;
            }

            var attributes = searchParameter
                .GetType()
#if NETCORE
                .GetTypeInfo()
#endif
                .GetCustomAttributes()
                .Where(q => q is IValidationAttribute)
                .ToList();

            var hasMultipleInstances = this
                ._searchItemCollection
                .GetSearchParameters()
                ?.Count(q => q.GetType() == searchParameter.GetType()) > 1;

            var allowMultipleInstances = !attributes.Any(q => q is AllowMultipleInstancesAttribute);

            Checker.IsTrue<AllowMultipleInstancesException>(hasMultipleInstances && !allowMultipleInstances, searchParameter.GetType().FullName);

            foreach (var attribute in attributes)
            {
                var isValid = ((IValidationAttribute)attribute).IsValid<TDocument>(searchParameter, out string errorMessage);

                Checker.IsFalse<SearchParameterIsInvalidException>(isValid, searchParameter.GetType().FullName, errorMessage);
            }
        }

        /// <summary>
        /// Set default parameters if necessary
        /// </summary>
        private void SetDefaultSearchParameters()
        {
            var systemParameter = this.ServiceProvider.GetService<ISystemParameter<TDocument>>();
            this._searchItemCollection.Add(systemParameter);

            if (!this._searchItemCollection.Contains<IOffsetParameter<TDocument>>())
            {
                var offsetParameter = this.ServiceProvider.GetService<IOffsetParameter<TDocument>>();
                offsetParameter.Value(0);
                this._searchItemCollection.Add(offsetParameter);
            }

            if (!this._searchItemCollection.Contains<ILimitParameter<TDocument>>())
            {
                var limitParameter = this.ServiceProvider.GetService<ILimitParameter<TDocument>>();
                limitParameter.Value(10);
                this._searchItemCollection.Add(limitParameter);
            }

            if (!this._searchItemCollection.Contains<ISortParameter<TDocument>>())
            {
                var sortParameter = this.ServiceProvider.GetService<ISortParameter<TDocument>>();
                sortParameter.FieldExpression(q => q.Score);
                sortParameter.Ascendent(false);
                this._searchItemCollection.Add(sortParameter);
            }

            if (!this._searchItemCollection.Contains<IWriteTypeParameter<TDocument>>())
            {
                var writeTypeParameter = this.ServiceProvider.GetService<IWriteTypeParameter<TDocument>>();
                writeTypeParameter.Value(WriteType.Json);
                this._searchItemCollection.Add(writeTypeParameter);
            }

            if (!this._searchItemCollection.Contains<IQueryParserParameter<TDocument>>())
            {
                var queryParserParameter = this.ServiceProvider.GetService<IQueryParserParameter<TDocument>>();
                queryParserParameter.Value(QueryParserType.Edismax);
                this._searchItemCollection.Add(queryParserParameter);
            }

            if (!this._searchItemCollection.Contains<IStandardQueryParameter<TDocument>>())
            {
                var standardQueryParameter = this.ServiceProvider.GetService<IStandardQueryParameter<TDocument>>();
                var searchQuery = this.ServiceProvider.GetService<SearchQuery<TDocument>>();
                standardQueryParameter.Value(searchQuery.AddValue("*:*", false));
                this._searchItemCollection.Add(standardQueryParameter);
            }

            // ReSharper disable once InvertIf
            if (!this._searchItemCollection.Contains<IDefaultFieldParameter<TDocument>>())
            {
                var defaultFieldParameter = this.ServiceProvider.GetService<IDefaultFieldParameter<TDocument>>();
                defaultFieldParameter.FieldExpression = q => q.Id;
                this._searchItemCollection.Add(defaultFieldParameter);
            }
        }

        /// <summary>
        /// Set default results if necessary
        /// </summary>
        private void SetDefaultSearchResults()
        {
            if (!this._searchItemCollection.Contains<IDocumentResult<TDocument>>())
            {
                var documentResult = this.ServiceProvider.GetService<IDocumentResult<TDocument>>();
                this._searchItemCollection.Add(documentResult);
            }

            // ReSharper disable once InvertIf
            if (!this._searchItemCollection.Contains<IInformationResult<TDocument>>())
            {
                var informationResult = this.ServiceProvider.GetService<IInformationResult<TDocument>>();
                this._searchItemCollection.Add(informationResult);
            }
        }

        /// <summary>
        /// Check if collection contains informed type
        /// </summary>
        /// <returns>True if contains informed type, otherwise false</returns>
        public bool Contains<TSearchItem>()
            where TSearchItem : ISearchItem
        {
            return this._searchItemCollection.Contains<TSearchItem>();
        }

        /// <summary>
        /// Add an item to search
        /// </summary>
        /// <param name="item">Parameter to add in the query</param>
        /// <returns>Itself</returns>
        public DocumentSearch<TDocument> Add(ISearchItem item)
        {
            Checker.IsNull(item);

            this.ValidateSearchItem(item);

            this._searchItemCollection.Add(item);

            return this;
        }

        /// <summary>
        /// Add items to search
        /// </summary>
        /// <param name="items">Parameter to add in the query</param>
        /// <returns>Itself</returns>
        public DocumentSearch<TDocument> AddRange(IEnumerable<ISearchItem> items)
        {
            Checker.IsNull(items);

            if (items.Any())
            {
                foreach (var item in items)
                {
                    Checker.IsNull(item);
                    this.ValidateSearchItem(item);
                }

                this._searchItemCollection.AddRange(items);
            }

            return this;
        }

        /// <summary>
        /// Handler name used in solr request
        /// </summary>
        /// <param name="name">Name to be used</param>
        /// <returns>Itself</returns>
        public DocumentSearch<TDocument> Handler(string name)
        {
            this._requestHandler = name;

            return this;
        }

        /// <summary>
        /// Execute search in the solr with informed parameters
        /// </summary>
        /// <returns>Solr result</returns>
        public IList<ISearchResult<TDocument>> Execute()
        {
            this.AddRange(this._solrExpressOptions.GlobalParameters);
            this.AddRange(this._solrExpressOptions.GlobalResultInterceptors);
            this.AddRange(this._solrExpressOptions.GlobalChangeBehaviours);

            this.SetDefaultSearchParameters();
            this.SetDefaultSearchResults();

            var jsonReader = this._searchItemCollection.Execute(this._requestHandler);
            var searchParameters = this._searchItemCollection.GetSearchParameters();
            var searchresults = this._searchItemCollection.GetSearchResults();

            var searchResultBuilder = this.ServiceProvider.GetService<SearchResultBuilder<TDocument>>();
            searchResultBuilder.Configure(jsonReader, searchParameters, searchresults);

            var searchResult = searchResultBuilder.Execute();

            jsonReader.Close();

            return searchResult;
        }
    }
}