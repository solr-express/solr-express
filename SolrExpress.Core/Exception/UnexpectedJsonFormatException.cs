namespace SolrExpress.Core.Exception
{
    public class UnexpectedJsonFormatException : System.Exception
    {
        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="json">Unexpected JSON string</param>
        public UnexpectedJsonFormatException(string json)
            : base(string.Format(Resource.UnexpectedJsonException, json))
        {
        }
    }
}
