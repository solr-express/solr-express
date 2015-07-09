namespace SolrExpress.Core.Exception
{
    public sealed class InvalidParameterTypeException : System.Exception
    {
        public InvalidParameterTypeException(string parameterType, string errorMessage) :
            base(string.Concat("Parameter \"", parameterType, "\" throwed an exception:\r\n\"", errorMessage, "\""))
        {
        }
    }
}
