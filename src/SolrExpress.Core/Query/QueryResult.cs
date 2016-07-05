using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Result;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// SOLR query result with fluent API
    /// </summary>
    public class QueryResult<TDocument>
        where TDocument : IDocument
    {
        private readonly string _jsonPlainText;
        private JObject _jsonObject;
        private long _offset;
        private long _limit;

        /// <summary>
        /// Resolver used to resolve classes dependency
        /// </summary>
        public IResolver Resolver { get; private set; }

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="resolver">Resolver used to resolve classes dependency</param>
        /// <param name="json">Result of the SOLR</param
        /// <param name="offset">Offset value of collection</param>
        /// <param name="limit">Limit value of collection</param>
        public QueryResult(IResolver resolver, string json, long offset, long limit)
        {
            Checker.IsNull(resolver);
            Checker.IsNullOrWhiteSpace(json);
            Checker.IsLowerThan(offset, 1);
            Checker.IsLowerThan(limit, 1);

            this.Resolver = resolver;
            this._jsonPlainText = json;
            this._offset = offset;
            this._limit = limit;
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

                convertJsonObject.Execute(this._jsonObject);
            }
            else
            {
                var convertJsonPlainText = result as IConvertJsonPlainText;

                if (convertJsonPlainText != null)
                {
                    convertJsonPlainText.Execute(this._jsonPlainText);
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
