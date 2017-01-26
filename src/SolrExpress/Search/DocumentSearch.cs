using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;

namespace SolrExpress.Search
{
    /// <summary>
    /// Document search engine
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public class DocumentSearch<TDocument>
        where TDocument : IDocument
    {
        private SolrExpressOptions<TDocument> _solrExpressOptions;
        private ISearchItemCollection<TDocument> _searchItemCollection;
        private string _requestHandler = RequestHandler.Select;

        public DocumentSearch(
            SolrExpressOptions<TDocument> solrExpressOptions,
            ISearchItemCollection<TDocument> searchItemCollection)
        {
            Checker.IsNull(solrExpressOptions);
            Checker.IsNull(searchItemCollection);

            this._solrExpressOptions = solrExpressOptions;
            this._searchItemCollection = searchItemCollection;
        }

        /// <summary>
        /// Execute search item validation
        /// </summary>
        /// <param name="item">Search item to validate</param>
        private void ValidateSearchItem(ISearchItem item)
        {
            if (!this._solrExpressOptions.FailFast)
            {
                return;
            }

            var searchParameter = (ISearchParameter)item;
            if (searchParameter != null)
            {
                var multipleInstances = !searchParameter.AllowMultipleInstances &&
                    this._searchItemCollection.Contains(searchParameter.GetType());

                //TODO: Create exception
                //Checker.IsTrue<AllowMultipleInstanceOfParameterTypeException>(multipleInstances, parameter.GetType().FullName);
            }

            var searchItemValidation = item as ISearchItemValidation;
            var mustValidate = searchItemValidation != null;

            if (item is IAnyParameter<TDocument>)
            {
                mustValidate = mustValidate && this._solrExpressOptions.CheckAnyParameter;
            }

            if (mustValidate)
            {
                bool isValid;
                string errorMessage;

                searchItemValidation.Validate(out isValid, out errorMessage);

                //TODO: Create exception
                //Checker.IsTrue<InvalidParameterTypeException>(!isValid, searchItemValidation.GetType().FullName, errorMessage);
            }
        }

        /// <summary>
        /// Set pagination parameters if necessary
        /// </summary>
        private void SetDefaultPaginationParameters()
        {
            if (!this._searchItemCollection.Contains<IOffsetParameter<TDocument>>())
            {
                //TODO: Need DI
                //var offsetParameter = this.Engine.GetService<IOffsetParameter<TDocument>>().Configure(0);
                //this._searchItemCollection.Add(offsetParameter);
            }

            if (!this._searchItemCollection.Contains<ILimitParameter<TDocument>>())
            {
                //TODO: Need DI
                //var limitParameter = this.Engine.GetService<ILimitParameter<TDocument>>().Configure(10);
                //this._searchItemCollection.Add(limitParameter);
            }
        }

        /// <summary>
        /// Set systemic parameters
        /// </summary>
        private void SetDefaultSystemParameters()
        {
            //TODO: https://github.com/solr-express/solr-express/issues/176
            throw new NotImplementedException();
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

            foreach (var item in items)
            {
                this.Add(item);
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
        /// Execute the search in the solr with informed parameters
        /// </summary>
        /// <returns>Solr result</returns>
        public SearchResult<TDocument> Execute()
        {
            this.AddRange(this._solrExpressOptions.GlobalParameters);
            this.AddRange(this._solrExpressOptions.GlobalQueryInterceptors);
            this.AddRange(this._solrExpressOptions.GlobalResultInterceptors);

            this.SetDefaultSystemParameters();
            this.SetDefaultPaginationParameters();

            var json = this._searchItemCollection.Execute(this._requestHandler);
            var searchParameters = this._searchItemCollection.GetParameters();

            return new SearchResult<TDocument>(searchParameters, json);
        }
    }
}