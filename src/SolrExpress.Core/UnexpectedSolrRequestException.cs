using System;

namespace SolrExpress.Core
{
    public class UnexpectedSolrRequestException : Exception
    {
        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="json">Unexpected JSON string</param>
        /// <param name="message">Server message</param>
        public UnexpectedSolrRequestException(string json, string message)
            : base(string.Format(Resource.UnexpectedSolrRequestException, json, message))
        {
        }
    }
}
