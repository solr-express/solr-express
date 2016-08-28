using System;

namespace SolrExpress.Core.Search.Result
{
    public class UnknownResolveResultBuilderException : Exception
    {
        public UnknownResolveResultBuilderException(string parameterType) :
            base(string.Format(Resource.UnknownResolveResultBuilderException, parameterType))
        {
        }
    }
}
