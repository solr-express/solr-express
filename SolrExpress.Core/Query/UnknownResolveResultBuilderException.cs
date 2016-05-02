using System;

namespace SolrExpress.Core.Query
{
    public class UnknownResolveResultBuilderException : Exception
    {
        public UnknownResolveResultBuilderException(string parameterType) :
            base(string.Format(Resource.UnknownResolveResultBuilderException, parameterType))
        {
        }
    }
}
