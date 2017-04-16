using System;

namespace SolrExpress.Search.Parameter.Validation
{
    public class AllowMultipleInstancesException : Exception
    {
        public AllowMultipleInstancesException(string parameterType) :
            base(string.Format(Resource.AllowMultipleInstancesException, parameterType))
        {
        }
    }
}
