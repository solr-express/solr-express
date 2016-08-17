using System;

namespace SolrExpress.Core
{
    public class UnexpectedJsonFormatException : Exception
    {
        /// <summary>
        /// Default constructor of class
        /// </summary>
        /// <param name="json">Unexpected JSON string</param>
        public UnexpectedJsonFormatException(string json)
            : base(string.Format(Resource.UnexpectedJsonException, json))
        {
        }
    }
}
