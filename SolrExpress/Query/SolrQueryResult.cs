using Newtonsoft.Json.Linq;

namespace SolrExpress.Query
{
    public class SolrQueryResult
    {
        private readonly string _jsonPlainText;
        private JObject _jsonObject;

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="json">Result of the SOLR</param>
        public SolrQueryResult(string json)
        {
            this._jsonPlainText = json;
        }

        /// <summary>
        /// Get a instance of the informed type with parsead json resulted from the search
        /// </summary>
        /// <typeparam name="T">Concrect class than implements the IResultBuilder interface</typeparam>
        /// <returns>Instance of T ready to be used</returns>
        public T Get<T>(T builder)
            where T : IResultBuilder
        {
            if (builder is IConvertJsonObject)
            {
                if (this._jsonObject == null)
                {
                    this._jsonObject = JObject.Parse(this._jsonPlainText);
                }

                ((IConvertJsonObject)builder).Execute(this._jsonObject);
            }
            else
            {
                ((IConvertJsonPlainText)builder).Execute(this._jsonPlainText);
            }

            return builder;
        }
    }
}
