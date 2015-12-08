namespace SolrExpress.Core.Exception
{
    public class UnexpectedJsonQueryException : System.Exception
    {
        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="json">Unexpected JSON string</param>
        public UnexpectedJsonQueryException(string json)
            : base(string.Format(Resource.UnexpectedJsonException, json))
        {
        }
    }
}
