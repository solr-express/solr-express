using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.Result;
using System.Collections.Generic;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// SOLR query result with fluent API
    /// </summary>
    public class QueryResult<TDocument> : IQueryResult<TDocument>
        where TDocument : IDocument
    {
        private readonly string _jsonPlainText;
        private readonly List<IParameter> _parameters;
        private JObject _jsonObject;

        /// <summary>
        /// Resolver used to resolve classes dependency
        /// </summary>
        public IResolver Resolver { get; private set; }

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="resolver">Resolver used to resolve classes dependency</param>
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <param name="json">Result of the SOLR</param
        public QueryResult(IResolver resolver, List<IParameter> parameters, string json)
        {
            Checker.IsNull(resolver);
            Checker.IsNullOrWhiteSpace(json);

            this.Resolver = resolver;
            this._parameters = parameters;
            this._jsonPlainText = json;
        }

        /// <summary>
        /// Get a instance of the informed type with parsed json resulted from the search
        /// </summary>
        /// <typeparam name="T">Concrete class that implements the IResultBuilder interface</typeparam>
        /// <returns>Instance of T ready to be used</returns>
        public T Get<T>(T result)
            where T : IResult
        {
            var convertJsonObject = result as IConvertJsonObject;

            if (convertJsonObject != null)
            {
                this._jsonObject = this._jsonObject ?? JObject.Parse(this._jsonPlainText);

                convertJsonObject.Execute(this._parameters, this._jsonObject);
            }
            else
            {
                var convertJsonPlainText = result as IConvertJsonPlainText;

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
    }
}
