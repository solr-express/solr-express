using System;

namespace SolrExpress.Search.Parameter.Validation
{
    public class SearchParameterIsInvalidException : Exception
    {
        public SearchParameterIsInvalidException(string parameterType, string errorMessage) :
            base(string.Format(Resource.SearchParameterIsInvalidException, parameterType, errorMessage))
        {
        }
    }
}
