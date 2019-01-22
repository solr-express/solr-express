using System;

namespace SolrExpress.Search.Parameter.Validation
{
    public class SearchParameterIsInvalidException : Exception
    {
        public SearchParameterIsInvalidException(string parameterName, string errorMessage) :
            base(string.Format(Resource.SearchParameterIsInvalidException, parameterName, errorMessage))
        {
        }
    }
}
