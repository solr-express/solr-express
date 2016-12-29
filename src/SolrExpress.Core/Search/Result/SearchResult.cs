using Newtonsoft.Json.Linq;
using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Utility;
using System.Collections.Generic;

namespace SolrExpress.Core.Search.Result
{
    /// <summary>
    /// SOLR query result with fluent API
    /// </summary>
    public class SearchResult<TDocument> : ISearchResult<TDocument>
        where TDocument : IDocument
    {
        private readonly string _jsonPlainText;
        private readonly List<ISearchParameter<TDocument>> _parameters;
        private JObject _jsonObject;

        /// <summary>
        /// Default constructor of class
        /// </summary>
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <param name="engine">Services container</param>
        /// <param name="json">Result of the SOLR</param
        public SearchResult(List<ISearchParameter<TDocument>> parameters, IEngine engine, string json)
        {
            Checker.IsNull(parameters);
            Checker.IsNull(engine);
            Checker.IsNullOrWhiteSpace(json);

            this._parameters = parameters;
            this._jsonPlainText = json;
            this.Engine = engine;
        }

        /// <summary>
        /// Get a instance of the informed type with parsed json resulted from the search
        /// </summary>
        /// <typeparam name="T">Concrete class that implements the IResultBuilder interface</typeparam>
        /// <returns>Instance of T ready to be used</returns>
        public T Get<T>(T result)
            where T : IResult
        {
            var convertJsonObject = result as IConvertJsonObject<TDocument>;

            if (convertJsonObject != null)
            {
                this._jsonObject = this._jsonObject ?? JObject.Parse(this._jsonPlainText);

                convertJsonObject.Execute(this._parameters, this._jsonObject);
            }
            else
            {
                var convertJsonPlainText = result as IConvertJsonPlainText<TDocument>;

                if (convertJsonPlainText != null)
                {
                    convertJsonPlainText.Execute(this._parameters, this._jsonPlainText);
                }
                else
                {
                    throw new UnknownResolveResultBuilderException(result.GetType().FullName);
                }
            }

            return result;
        }

        /// <summary>
        /// Services container
        /// </summary>
        public IEngine Engine { get; private set; }
    }
}
