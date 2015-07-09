namespace SolrExpress.Core.Exception
{
    public class UnknownResolveResultBuilderException : System.Exception
    {
        public UnknownResolveResultBuilderException(string parameterType) :
            base(string.Concat("Unknown resolve of the result builder type \"", parameterType, "\" because this type doesn't implement IConvertJsonObject or IConvertJsonPlainText"))
        {
        }
    }
}
