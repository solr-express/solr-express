namespace SolrExpress.Exception
{
    public class AllowMultipleInstanceOfParameterTypeException : System.Exception
    {
        public AllowMultipleInstanceOfParameterTypeException(string parameterName) :
            base(string.Concat("Parameter \"", parameterName, "\" is not allowed because another instance of the same type was add"))
        {
        }
    }
}
