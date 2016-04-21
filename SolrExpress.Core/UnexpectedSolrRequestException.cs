using System;

namespace SolrExpress.Core
{
    public class UnexpectedSolrRequestException : Exception
    {
        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="json">Unexpected JSON string</param>
        public UnexpectedSolrRequestException(string json)
            : base(string.Format(Resource.UnexpectedSolrRequestException, json))
        {
        }
    }
}
