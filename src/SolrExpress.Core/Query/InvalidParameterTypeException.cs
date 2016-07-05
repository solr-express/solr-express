namespace SolrExpress.Core.Query
{
    public sealed class InvalidParameterTypeException : System.Exception
    {
        public InvalidParameterTypeException(string parameterType, string errorMessage) :
            base(string.Format(Resource.InvalidParameterTypeException, parameterType, errorMessage))
        {
        }
    }
}
