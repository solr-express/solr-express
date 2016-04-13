using System;

namespace SolrExpress.Core
{
    public class UnexpectedJsonQueryException : Exception
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
