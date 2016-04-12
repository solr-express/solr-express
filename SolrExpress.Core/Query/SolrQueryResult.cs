using Newtonsoft.Json.Linq;
using SolrExpress.Core.Result;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// SOLR query result with fluent API
    /// </summary>
    public class SolrQueryResult<TDocument>
        where TDocument : IDocument
    {
        private readonly string _jsonPlainText;
        private JObject _jsonObject;

        /// <summary>
        /// Resolver used to resolve classes dependency
        /// </summary>
        public IResolver Resolver { get; private set; }

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="resolver">Resolver used to resolve classes dependency</param>
        /// <param name="json">Result of the SOLR</param>
        public SolrQueryResult(IResolver resolver, string json)
        {
            Checker.IsNull(resolver);
            Checker.IsNullOrWhiteSpace(json);

            this.Resolver = resolver;
            this._jsonPlainText = json;
        }

        /// <summary>
        /// Get a instance of the informed type with parsed json resulted from the search
        /// </summary>
        /// <typeparam name="T">Concrete class that implements the IResultBuilder interface</typeparam>
        /// <returns>Instance of T ready to be used</returns>
        public T Get<T>(T builder)
            where T : IResultBuilder
        {
            if (builder is IConvertJsonObject)
            {
                this._jsonObject = this._jsonObject ?? JObject.Parse(this._jsonPlainText);

                ((IConvertJsonObject)builder).Execute(this._jsonObject);
            }
            else if (builder is IConvertJsonPlainText)
            {
                ((IConvertJsonPlainText)builder).Execute(this._jsonPlainText);
            }
            else
            {
                throw new UnknownResolveResultBuilderException(builder.GetType().Name);
            }

            return builder;
        }
    }
}
