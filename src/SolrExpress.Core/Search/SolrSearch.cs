using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Search.Result;
using SolrExpress.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolrExpress.Core.Search
{
    /// <summary>
    /// SOLR queryable
    /// </summary>
    public sealed class SolrSearch<TDocument> : ISolrSearch<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// List of itens
        /// </summary>
        private readonly List<ISearchItem> _items = new List<ISearchItem>();

        /// <summary>
        /// Handler name used in solr request
        /// </summary>
        private string _handlerName = RequestHandler.Select;

        /// <summary>
        /// Default constructor of class
        /// </summary>
        /// <param name="options">SolrExpress options</param>
        /// <param name="engine">Services container</param>
        public SolrSearch(DocumentCollectionOptions<TDocument> options, IEngine engine)
        {
            Checker.IsNull(options);
            Checker.IsNull(engine);

            this.Options = options;
            this.Engine = engine;
        }

        /// <summary>
        /// Validate search parameter
        /// </summary>
        /// <param name="item">Search item to validate</param>
        private void ValidateSearchParameter(ISearchItem item)
        {
            if (item is ISearchParameter)
            {
                var parameter = (ISearchParameter)item;

                var multipleInstances = !parameter.AllowMultipleInstances && this._items
                    .Any(q => (q.GetType() == parameter.GetType()));

                Checker.IsTrue<AllowMultipleInstanceOfParameterTypeException>(multipleInstances, parameter.GetType().FullName);

                var parameterValidation = parameter as IValidation;

                var mustValidate = this.Options.FailFast && parameterValidation != null;

                if (parameter is IAnyParameter<TDocument>)
                {
                    mustValidate = mustValidate && this.Options.CheckAnyParameter && parameter is IAnyParameter<TDocument>;
                }

                if (mustValidate)
                {
                    bool isValid;
                    string errorMessage;

                    parameterValidation.Validate(out isValid, out errorMessage);

                    Checker.IsTrue<InvalidParameterTypeException>(!isValid, parameterValidation.GetType().FullName, errorMessage);
                }
            }
        }

        /// <summary>
        /// Set pagination parameters if it was not set
        /// </summary>
        private void SetDefaultPaginationParameters()
        {
            var offsetParameter = (IOffsetParameter<TDocument>)this._items.FirstOrDefault(q => q is IOffsetParameter<TDocument>);

            if (offsetParameter == null)
            {
                offsetParameter = this.Engine.GetService<IOffsetParameter<TDocument>>().Configure(0);
                this._items.Add(offsetParameter);
            }

            var limitParameter = (ILimitParameter<TDocument>)this._items.FirstOrDefault(q => q is ILimitParameter<TDocument>);

            if (limitParameter == null)
            {
                limitParameter = this.Engine.GetService<ILimitParameter<TDocument>>().Configure(10);
                this._items.Add(limitParameter);
            }
        }

        /// <summary>
        /// Add an item to search
        /// </summary>
        /// <param name="item">Parameter to add in the query</param>
        ISolrSearch<TDocument> ISolrSearch<TDocument>.Add(ISearchItem item)
        {
            Checker.IsNull(item);

            this.ValidateSearchParameter(item);

            this._items.Add(item);

            return this;
        }

        /// <summary>
        /// Add items to search
        /// </summary>
        /// <param name="items">Parameter to add in the query</param>
        ISolrSearch<TDocument> ISolrSearch<TDocument>.AddRange(IEnumerable<ISearchItem> items)
        {
            Checker.IsNull(items);

            foreach (var item in items)
            {
                this.ValidateSearchParameter(item);

                this._items.Add(item);
            }

            return this;
        }

        /// <summary>
        /// Add a result interceptor to the queryable
        /// </summary>
        ISolrSearch<TDocument> ISolrSearch<TDocument>.Add<TQueryInterceptor>(Action<TQueryInterceptor> builder)
        {
            var interceptor = new TQueryInterceptor();

            builder?.Invoke(interceptor);

            this._items.Add(interceptor);

            return this;
        }

        /// <summary>
        /// Add a result interceptor to the queryable
        /// </summary>
        ISolrSearch<TDocument> ISolrSearch<TDocument>.Add<TResultInterceptor>(Action<IResultInterceptor> builder)
        {
            var interceptor = new TResultInterceptor();

            builder?.Invoke(interceptor);

            this._items.Add(interceptor);

            return this;
        }

        /// <summary>
        /// Determines whether an element is in current list
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns>Returns true if item is found in list, otherwise false</returns>
        public bool Contains(ISearchItem item) => this._items.Contains(item);

        /// <summary>
        /// Handler name used in solr request
        /// </summary>
        /// <param name="name">Name to be used</param>
        /// <returns>Itself</returns>
        public ISolrSearch<TDocument> SetHandler(string name)
        {
            Checker.IsNullOrWhiteSpace(name);

            this._handlerName = name;

            return this;
        }

        /// <summary>
        /// Execute the search in the solr with informed parameters
        /// </summary>
        /// <returns>Solr result</returns>
        public ISearchResult<TDocument> Execute()
        {
            var systemParameter = this.Engine.GetService<ISystemParameter<TDocument>>();
            var parameterCollection = this.Engine.GetService<ISearchParameterCollection<TDocument>>();

            var sync = new object();

            Parallel.ForEach(this.Options.GlobalParameters, item =>
            {
                lock (sync)
                {
                    ((ISolrSearch<TDocument>)this).Add(item);
                }
            });
            Parallel.ForEach(this.Options.GlobalQueryInterceptors, item =>
            {
                lock (sync)
                {
                    ((ISolrSearch<TDocument>)this).Add(item);
                }
            });
            Parallel.ForEach(this.Options.GlobalResultInterceptors, item =>
            {
                lock (sync)
                {
                    ((ISolrSearch<TDocument>)this).Add(item);
                }
            });

            systemParameter.Configure();
            this._items.Add(systemParameter);

            this.SetDefaultPaginationParameters();

            var searchParameters = this._items.OfType<ISearchParameter>().ToList();

            parameterCollection.Add(searchParameters);
            var query = parameterCollection.Execute();

            Parallel.ForEach(this._items.OfType<ISearchInterceptor>(), interceptor =>
            {
                lock (sync)
                {
                    interceptor.Execute(ref query);
                }
            });

            var solrConnection = this.Engine.GetService<ISolrConnection>();
            solrConnection.HostAddress = this.Options.HostAddress;
            var json = solrConnection.Get(this._handlerName, query);

            Parallel.ForEach(this._items.OfType<IResultInterceptor>(), interceptor =>
            {
                lock (sync)
                {
                    interceptor.Execute(ref query);
                }
            });

            return new SearchResult<TDocument>(searchParameters, this.Engine, json);
        }

        /// <summary>
        /// Gets the number of elements
        /// </summary>
        public int Count => this._items.Count;

        /// <summary>
        /// SolrExpress options
        /// </summary>
        public DocumentCollectionOptions<TDocument> Options { get; private set; }

        /// <summary>
        /// Services container
        /// </summary>
        public IEngine Engine { get; private set; }
    }
}
