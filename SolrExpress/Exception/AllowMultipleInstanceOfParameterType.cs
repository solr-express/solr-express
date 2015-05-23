namespace SolrExpress.Exception
{
    public class AllowMultipleInstanceOfParameterType : System.Exception
    {
        public AllowMultipleInstanceOfParameterType(string parameterName) :
            base(string.Concat("Parameter \"", parameterName, "\" is not allowed because another instance of the same type was add"))
        {
        }
    }
}
