namespace SolrExpress.Core.Exception
{
    public class UnknownResolveResultBuilderException : System.Exception
    {
        public UnknownResolveResultBuilderException(string parameterType) :
            base(string.Concat("Unknown resolve the result builder type \"", parameterType, "\" because this type don't implement IConvertJsonObject or IConvertJsonPlainText"))
        {
        }
    }
}
