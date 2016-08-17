using System;

namespace SolrExpress.Core
{
    public class UnexpectedDependencyInjectionMappingException : Exception
    {
        /// <summary>
        /// Default constructor of class
        /// </summary>
        /// <param name="sourceName">Source name</param>
        public UnexpectedDependencyInjectionMappingException(string sourceName)
            : base(string.Format(Resource.UnexpectedDependencyInjectionMappingException, sourceName))
        {
        }
    }
}
