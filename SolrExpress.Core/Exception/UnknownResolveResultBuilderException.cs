namespace SolrExpress.Core.Exception
{
    public class UnknownResolveResultBuilderException : System.Exception
    {
        public UnknownResolveResultBuilderException(string parameterType) :
            base(string.Format(Resource.UnknownResolveResultBuilderException, parameterType))
        {
        }
    }
}
